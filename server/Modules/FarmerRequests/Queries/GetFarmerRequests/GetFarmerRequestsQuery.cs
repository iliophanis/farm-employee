using MediatR;

namespace server.Modules.EmployeeRequests.Queries.GetFarmerRequests
{
    public record GetFarmerRequestsQuery(int PageSize, int CurrentPage, string? Filter, string Type) : IRequest<GetFarmerRequestsResponseDto>;
}