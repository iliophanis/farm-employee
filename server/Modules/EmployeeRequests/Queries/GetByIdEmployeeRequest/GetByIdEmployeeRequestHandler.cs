using MediatR;
using server.Modules.Common.Exceptions;
using server.Modules.EmployeeRequests.Dto;

namespace server.Modules.EmployeeRequests.Queries.GetByIdEmployeeRequest
{
    public class GetByIdEmployeeRequestHandler : IRequestHandler<GetByIdEmployeeRequestQuery, GetByIdEmployeeRequestResponseDto>
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public GetByIdEmployeeRequestHandler(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<GetByIdEmployeeRequestResponseDto> Handle(GetByIdEmployeeRequestQuery request, CancellationToken cancellationToken)
        {
            var employeeRequest = await _context.EmployeeRequests
                            .Where(x => x.Id == request.employeeRequestId && x.EmployeeId == request.employeeId)
                            .FirstOrDefaultAsync(cancellationToken);

            if (employeeRequest is null) throw new NotFoundException("Request not found.");

            var subEmployees = await _context.SubEmployees
                            .Where(x => x.EmployeeRequestId == request.employeeRequestId)
                            .ToListAsync(cancellationToken);

            var employee = await _context.Employees
                    .Include(x => x.User)
                    .Where(x => x.Id == employeeRequest.EmployeeId)
                    .FirstOrDefaultAsync(cancellationToken);            

            return new GetByIdEmployeeRequestResponseDto(employee.User.FirstName, employee.User.LastName, employee.User.Email, employeeRequest.MessageSent, employeeRequest.PaymentStatus, employeeRequest.PaymentMethod, subEmployees);
        }
    }
}