using MediatR;
using server.Modules.Common.Exceptions;
using server.Modules.EmployeeRequests.Dto;
using server.Data.Entities;

namespace server.Modules.EmployeeRequests.Queries.GetByIdEmployeeRequest
{
    public class GetByIdEmployeeRequestHandler : IRequestHandler<GetByIdEmployeeRequestQuery, GetByIdEmployeeRequestDto>
    {
        private readonly DataContext _context;

        public GetByIdEmployeeRequestHandler(DataContext context, IConfiguration configuration)
        {
            _context = context;
        }

        public async Task<GetByIdEmployeeRequestDto> Handle(GetByIdEmployeeRequestQuery request, CancellationToken cancellationToken)
        {
            var employeeRequest = await _context.EmployeeRequests
                            .Include(x => x.Employee).ThenInclude(x => x.User)
                            .Where(x => x.Id == request.employeeRequestId)
                            .FirstOrDefaultAsync(cancellationToken);

            if (employeeRequest is null) throw new NotFoundException("Request not found.");

            var subEmployee = await _context.SubEmployees
                            .Where(x => x.EmployeeRequestId == request.employeeRequestId)
                            .ToListAsync(cancellationToken);

            var dto = employeeRequest.ToGetByIdEmployeeRequestDto();
            dto.Subemployees = subEmployee;

            return dto;
        }
    }
}