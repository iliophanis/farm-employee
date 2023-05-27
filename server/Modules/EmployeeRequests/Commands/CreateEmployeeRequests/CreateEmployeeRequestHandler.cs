using MediatR;
using server.Modules.Common.Exceptions;
using server.Data.Entities;
using server.Modules.Common.Responses;

namespace server.Modules.EmployeeRequests.Commands.CreateEmployeeRequest
{
    public class CreateEmployeeRequestHandler : IRequestHandler<CreateEmployeeRequestCommand, CommandResponse<string>>
    {
        private readonly DataContext _context;

        public CreateEmployeeRequestHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<CommandResponse<string>> Handle(CreateEmployeeRequestCommand request, CancellationToken cancellationToken)
        {
            var dto = request.CreateEmployeeRequestDto;

            var user = await _context.Users
                .Where(u => u.Email == dto.UserName)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(cancellationToken);

            if (user is null) throw new NotFoundException($"User with userName {dto.UserName} not found.");

            var Package = new Package{
                Name = dto.Package.Name,
                Price = dto.Package.Price,
                Discount = dto.Package.Discount,
                MaxRequests = dto.Package.MaxRequests
            };

            var EmployeeId = await _context.Employees
                            .Where(f => f.UserId == user.Id)
                            .Select(x => x.Id)
                            .FirstOrDefaultAsync(cancellationToken);
            
            if (EmployeeId == 0) throw new NotFoundException($"Employee with userName {dto.UserName} not found.");

            var EmployeeRequest = new EmployeeRequest{
                    MessageSent = true,
                    PaymentStatus = dto.EmployeeRequest.PaymentStatus,
                    PaymentMethod = dto.EmployeeRequest.PaymentMethod,
                    EmployeeId = EmployeeId,
                    RequestId = dto.requestId,
                    Package = Package
            };

            await _context.EmployeeRequests.AddAsync(EmployeeRequest, cancellationToken);

            var contactInfoId = await _context.Employees
                                .Where(x => x.Id == EmployeeId)
                                .Select(x => x.ContactInfo.Id)
                                .FirstOrDefaultAsync(cancellationToken);

            foreach (var SubEmployee in dto.SubEmployee)
            {
                var subEmpl = new SubEmployee
                {
                    FirstName = SubEmployee.FirstName,
                    LastName = SubEmployee.LastName,
                    Email = SubEmployee.Email,
                    EmployeeRequest = EmployeeRequest,
                    ContactInfoId = contactInfoId
                };
                _context.SubEmployees.Add(subEmpl);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return new CommandResponse<string>().WithData("Farmer has been notified");  
        }

    }

}