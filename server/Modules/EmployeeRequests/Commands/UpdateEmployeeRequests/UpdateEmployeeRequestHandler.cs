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

            var employee = await _context.Employees
                            .Where(x => x.UserId == user.Id)
                            .FirstOrDefaultAsync(cancellationToken);

            var emplRequest = await _context.EmployeeRequests
                            .Where(x => x.Id == dto.RequestId)
                            .Include(x => x.Package)
                            .Include(x => x.SubEmployees)
                            .FirstOrDefaultAsync(cancellationToken);

            if (emplRequest is null) throw new NotFoundException($"Request with id {dto.RequestId} not found.");

            _context.Attach(emplRequest);

            emplRequest.Package.Name = dto.Package.Name.Equals("string") ? emplRequest.Package.Name : dto.Package.Name;
            emplRequest.Package.Price = dto.Package.Price.Equals("string") ? emplRequest.Package.Price : dto.Package.Price;
            emplRequest.Package.Discount = dto.Package.Discount.Equals(0) ? emplRequest.Package.Discount : dto.Package.Discount;
            emplRequest.Package.MaxRequests = dto.Package.MaxRequests.Equals(0) ? emplRequest.Package.MaxRequests : dto.Package.MaxRequests;
             
            emplRequest.MessageSent = true;
            emplRequest.PaymentMethod = dto.EmployeeRequest.PaymentMethod.Equals(null) ? emplRequest.PaymentMethod : dto.EmployeeRequest.PaymentMethod;
            emplRequest.PaymentStatus = dto.EmployeeRequest.PaymentStatus.Equals(null) ? emplRequest.PaymentStatus : dto.EmployeeRequest.PaymentStatus;
           
            foreach (var SubEmployee in dto.SubEmployee)
            {
                var subEmpl = await _context.SubEmployees
                        .Where(x => x.Email == SubEmployee.Email)
                        .Where(x => x.EmployeeRequestId == dto.RequestId)
                        .FirstOrDefaultAsync(cancellationToken);

                if (subEmpl is null) continue;               

                subEmpl.FirstName = SubEmployee.FirstName.Equals("string") ? subEmpl.FirstName : SubEmployee.FirstName;
                subEmpl.LastName = SubEmployee.LastName.Equals("string") ? subEmpl.LastName : SubEmployee.LastName;

                _context.Add(subEmpl);

            }
            
            await _context.SaveChangesAsync(cancellationToken);

            return new CommandResponse<string>().WithData($"Successful update in request with Id {dto.RequestId}");  
        }

    }

}