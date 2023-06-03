using MediatR;
using server.Data.Entities;
using server.Modules.EmployeeRequests.Dto;

namespace server.Modules.EmployeeRequests.Queries.GetListEmployeeRequest
{
    public record GetListEmployeeRequestQuery(int requestId) : IRequest<List<GetListEmployeeRequestDto>>;
}