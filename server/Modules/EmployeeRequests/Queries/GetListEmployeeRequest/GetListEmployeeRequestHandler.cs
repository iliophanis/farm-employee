using MediatR;
using server.Data.Entities;
using server.Modules.Common.Exceptions;
using server.Modules.EmployeeRequests.Dto;

namespace server.Modules.EmployeeRequests.Queries.GetListEmployeeRequest
{
    public class GetListEmployeeRequestHandler : IRequestHandler<GetListEmployeeRequestQuery, GetListEmployeeRequestResponseDto>
    {
        private readonly DataContext _context;

        public GetListEmployeeRequestHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<GetListEmployeeRequestResponseDto> Handle(GetListEmployeeRequestQuery request, CancellationToken cancellationToken)
        {
             var employeeRequest = await _context.EmployeeRequests
                            .Where(x => x.Id == request.employeeRequestId)
                            .FirstOrDefaultAsync(cancellationToken);

            if (employeeRequest is null) throw new NotFoundException("Request not found.");

            var subEmployees = await _context.SubEmployees
                            .Where(x => x.EmployeeRequestId == employeeRequest.Id)
                            .ToListAsync(cancellationToken);

            var employee = await _context.Employees
                    .Include(x => x.User)
                    .Where(x => x.Id == employeeRequest.EmployeeId)
                    .FirstOrDefaultAsync(cancellationToken);            

            return new GetListEmployeeRequestResponseDto(employee.User.FirstName, employee.User.LastName, employee.User.Email, employeeRequest.MessageSent, employeeRequest.PaymentStatus, employeeRequest.PaymentMethod, subEmployees);
        }
    }
}