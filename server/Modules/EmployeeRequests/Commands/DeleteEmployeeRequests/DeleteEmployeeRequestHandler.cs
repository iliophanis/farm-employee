using MediatR;
using server.Modules.Common.Exceptions;
using server.Modules.Common.Responses;
using server.Modules.Common.Services;

namespace server.Modules.EmployeeRequests.Commands.DeleteEmployeeRequest
{
    public class DeleteEmployeeRequestHandler : IRequestHandler<DeleteEmployeeRequestCommand, CommandResponse<string>>
    {
        private readonly DataContext _context;

        private readonly CurrentUserService _currentUserService;

        public DeleteEmployeeRequestHandler(DataContext context, CurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<CommandResponse<string>> Handle(DeleteEmployeeRequestCommand request, CancellationToken cancellationToken)
        {
            var employeeId = _currentUserService.GetEmployeeId();

            var employeeRequest = await _context.EmployeeRequests
                        .Where(r => r.Id == request.EmployeeRequestId)
                        .FirstOrDefaultAsync(cancellationToken);

            if (employeeRequest is null) throw new NotFoundException("Request not found");
            if (employeeRequest.EmployeeId != employeeId) throw new BadRequestException("Changes are allowed only from owner");

            if (employeeRequest.PackageId != null)
            {
                var package = await _context.Packages
                                    .Where(p => p.Id == employeeRequest.PackageId)
                                    .FirstOrDefaultAsync(cancellationToken);
                _context.Packages.Remove(package);
            }


            var subEmployees = await _context.SubEmployees
                                .Where(s => s.EmployeeRequestId == employeeRequest.Id)
                                .ToListAsync(cancellationToken);

            if (subEmployees.Any())
            {
                _context.SubEmployees.RemoveRange(subEmployees);
            }

            _context.EmployeeRequests.Remove(employeeRequest);

            await _context.SaveChangesAsync(cancellationToken);

            return new CommandResponse<string>().WithData("Employee Request deleted successfully!");

        }

    }

}