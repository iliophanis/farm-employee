using MediatR;
using server.Modules.Common.Exceptions;
using server.Data.Entities;
using server.Modules.Common.Responses;
using server.Modules.Common.Services;
using server.Modules.Common.Services.Email;

namespace server.Modules.EmployeeRequests.Commands.SubmitRequest
{
    public class SubmitRequestHandler : IRequestHandler<SubmitRequestCommand, CommandResponse<string>>
    {
        private readonly DataContext _context;
        private readonly CurrentUserService _currentUserService;

        private readonly EmailService _emailService;

        public SubmitRequestHandler(DataContext context, CurrentUserService currentUserService, EmailService emailService)
        {
            _context = context;
            _currentUserService = currentUserService;
            _emailService = emailService;
        }

        public async Task<CommandResponse<string>> Handle(SubmitRequestCommand request, CancellationToken cancellationToken)
        {
            var dto = request.SubmitRequestDto;
            var jobRequest = await _context.Requests.Where(x => x.Id == dto.RequestId)
            .Include(x => x.Farmer).ThenInclude(x => x.User)
            .FirstOrDefaultAsync(cancellationToken);
            if (jobRequest == null) throw new BadRequestException("Request not existed");
            var employeeId = _currentUserService.GetEmployeeId();
            if (employeeId == null) throw new BadRequestException("Employee not existed");
            var employee = await _context.Employees
                                .Where(x => x.Id == employeeId)
                                .Include(x => x.User)
                                .Include(x => x.ContactInfo)
                                .FirstOrDefaultAsync(cancellationToken);
            var emailToFarmer = new EmailMessage
            {
                ToEmail = new List<string> { jobRequest.Farmer.User.Email },
                Subject = $"Νέο Αίτημα Υπαλλήλου στο {jobRequest.Id} που επιθυμεί να αναλάβει την εργασία",
                HtmlBody = $"Στοιχεία Επικοινωνίας <br><hr> Ονοματεπώνυμο : {employee.User.DisplayName}<br> <hr> Email : {employee.User.Email} <br> <hr> Τηλέφωνο Επικοινωνίας : {employee.ContactInfo.MobilePhoneNo} <br> <hr> Διεύθυνση : {employee.ContactInfo.DisplayName} "
            };
            await _emailService.SendEmailAsync(emailToFarmer);

            var employeeRequest = new EmployeeRequest
            {
                MessageSent = true,
                PaymentStatus = PaymentStatus.pendingPayment,
                PaymentMethod = null,
                EmployeeId = (int)employeeId,
                RequestId = dto.RequestId,
            };

            await _context.EmployeeRequests.AddAsync(employeeRequest, cancellationToken);
            if (dto.SubEmployees.Any())
            {
                //TODO ADD CONTACTINFO TO DB FOR SUBEMPLOYEES

                foreach (var subEmp in dto.SubEmployees)
                {
                    var subEmpl = new SubEmployee
                    {
                        FirstName = subEmp.FirstName,
                        LastName = subEmp.LastName,
                        Email = subEmp.Email,
                        EmployeeRequest = employeeRequest,
                        ContactInfoId = null
                    };
                    _context.SubEmployees.Add(subEmpl);
                }
            }


            await _context.SaveChangesAsync(cancellationToken);

            return new CommandResponse<string>().WithData("Employee Request have been submitted successfully");
        }

    }

}