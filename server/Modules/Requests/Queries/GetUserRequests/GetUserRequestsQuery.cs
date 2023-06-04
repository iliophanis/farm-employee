using MediatR;
using server.Modules.Requests.Dto;

namespace server.Modules.Requests.Queries.GetUserRequests
{
    public record GetUserRequestsQuery(decimal? MinLat, decimal? MaxLat, decimal? MinLon, decimal? MaxLon) : IRequest<List<GetUserRequestDto>>;
}