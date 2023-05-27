using MediatR;
using server.Modules.Common.Exceptions;
using server.Modules.Common.Responses;

namespace server.Modules.EmployeeRequests.Commands.DeleteEmployeeRequest
{
    public class DeleteEmployeeRequestHandler : IRequestHandler<DeleteEmployeeRequestCommand, CommandResponse<string>>
    {
        private readonly DataContext _context;

        public DeleteEmployeeRequestHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<CommandResponse<string>> Handle(DeleteEmployeeRequestCommand request, CancellationToken cancellationToken)
        {
            var dto = request.DeleteEmployeeRequestDto;
            var user = await _context.Users
                .Where(u => u.Email == dto.UserName)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(cancellationToken);

            if (user is null) throw new NotFoundException($"User with userName {dto.UserName} not found.");

            var employee = await _context.Employees
                            .Where(f => f.UserId == user.Id)
                            .FirstOrDefaultAsync(cancellationToken);

            var employeeRequest = await _context.EmployeeRequests
                        .Where(r => r.Id == dto.EmployeeRequestId)
                        .FirstOrDefaultAsync(cancellationToken);

            if (employeeRequest is null) throw new NotFoundException("Request not found");
            if (employeeRequest.EmployeeId != employee.Id) throw new BadRequestException("Changes are allowed only from owner");

            if (employeeRequest is not null)
            {
                var package = await _context.Packages
                                    .Where(p => p.Id == employeeRequest.PackageId)
                                    .FirstOrDefaultAsync(cancellationToken);

                var subEmployee = await _context.SubEmployees
                                    .Where(s => s.EmployeeRequestId == employeeRequest.Id)
                                    .ToListAsync(cancellationToken);

                if (subEmployee is not null)
                {
                    foreach (var subEmpl in subEmployee)
                    {
                        _context.SubEmployees.Remove(subEmpl);
                    }
                }

                _context.Packages.Remove(package);
                
                _context.EmployeeRequests.Remove(employeeRequest);

                await _context.SaveChangesAsync(cancellationToken);

            }     

            return new CommandResponse<string>().WithData("Farmer has been notified");  

        }

    }

}