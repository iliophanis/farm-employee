using MediatR;
using server.Modules.EmployeeRequests.Dto;

namespace server.Modules.EmployeeRequests.Queries.GetByIdEmployeeRequest
{
    public record GetByIdEmployeeRequestQuery(int employeeRequestId, int employeeId) : IRequest<GetByIdEmployeeRequestResponseDto>;
}