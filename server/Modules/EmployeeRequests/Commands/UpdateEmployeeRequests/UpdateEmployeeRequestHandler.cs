using MediatR;
using server.Modules.Common.Exceptions;
using server.Modules.Common.Responses;
using server.Data.Entities;

namespace server.Modules.EmployeeRequests.Commands.UpdateEmployeeRequest
{
    public class UpdateEmployeeRequestHandler : IRequestHandler<UpdateEmployeeRequestCommand, CommandResponse<string>>
    {
        private readonly DataContext _context;

        public UpdateEmployeeRequestHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<CommandResponse<string>> Handle(UpdateEmployeeRequestCommand request, CancellationToken cancellationToken)
        {
            var dto = request.UpdateEmployeeRequestDto;
            var user = await _context.Users
                .Where(u => u.Email == dto.UserName)
                .Include(u => u.Role)                
                .FirstOrDefaultAsync(cancellationToken);

            if (user is null) throw new NotFoundException($"User with userName {dto.UserName} not found.");

            var emplRequest = await _context.EmployeeRequests
                            .Where(x => x.Id == dto.RequestId)
                            .Include(x => x.Package)
                            .Include(x => x.SubEmployees)
                            .FirstOrDefaultAsync(cancellationToken);

            if (emplRequest is null) throw new NotFoundException($"Request with id {dto.RequestId} not found.");

            _context.Attach(emplRequest);

            emplRequest.Package.Name = dto.Package.Name ?? emplRequest.Package.Name;
            emplRequest.Package.Price = dto.Package.Price.Equals(0) ? emplRequest.Package.Price : dto.Package.Price;
            emplRequest.Package.Discount = dto.Package.Discount.Equals(0) ? emplRequest.Package.Discount : dto.Package.Discount;
            emplRequest.Package.MaxRequests = dto.Package.MaxRequests.Equals(0) ? emplRequest.Package.MaxRequests : dto.Package.MaxRequests;
             
            emplRequest.MessageSent = true;
            emplRequest.PaymentMethod = dto.EmployeeRequest?.PaymentMethod ?? emplRequest.PaymentMethod;
            emplRequest.PaymentStatus = dto.EmployeeRequest?.PaymentStatus ?? emplRequest.PaymentStatus;


            foreach (var SubEmployee in dto.SubEmployee)
            {
                var subEmpl = emplRequest.SubEmployees
                        .Where(x => x.Email == SubEmployee.Email && x.EmployeeRequestId == dto.RequestId)
                        .FirstOrDefault();

                if (subEmpl is null) continue;

                subEmpl.FirstName = SubEmployee.FirstName ?? subEmpl.FirstName;
                subEmpl.LastName = SubEmployee.LastName ?? subEmpl.LastName;
            }   

            await _context.SaveChangesAsync(cancellationToken);       
            

            return new CommandResponse<string>().WithData($"Successful update in request with Id {dto.RequestId}");  
        }

    }

}