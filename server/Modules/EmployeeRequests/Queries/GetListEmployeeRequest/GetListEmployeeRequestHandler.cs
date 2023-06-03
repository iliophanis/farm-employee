using MediatR;
using server.Data.Entities;
using server.Modules.Common.Exceptions;
using server.Modules.EmployeeRequests.Dto;

namespace server.Modules.EmployeeRequests.Queries.GetListEmployeeRequest
{
    public class GetListEmployeeRequestHandler : IRequestHandler<GetListEmployeeRequestQuery, List<GetListEmployeeRequestDto>>
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public GetListEmployeeRequestHandler(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<GetListEmployeeRequestDto>> Handle(GetListEmployeeRequestQuery request, CancellationToken cancellationToken)
        {
             var employeeRequest = await _context.EmployeeRequests
                            .Include(x => x.Employee).ThenInclude(x => x.User)
                            .Include(x => x.SubEmployees)
                            .Where(x => x.RequestId == request.requestId)
                            .Select(x => new GetListEmployeeRequestDto(x.Employee.User.FirstName, x.Employee.User.LastName, x.Employee.User.Email))
                            .ToListAsync(cancellationToken);

            if (employeeRequest is null) throw new NotFoundException("Request not found."); 

            return employeeRequest;
        }
    }
}