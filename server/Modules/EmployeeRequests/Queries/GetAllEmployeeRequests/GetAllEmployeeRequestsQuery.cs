using MediatR;

namespace server.Modules.EmployeeRequests.Queries.GetAllEmployeeRequests
{
    public record GetAllEmployeeRequestsQuery(int PageSize, int CurrentPage, string? Filter, string Type) : IRequest<GetAllEmployeeRequestsResponseDto>;
}