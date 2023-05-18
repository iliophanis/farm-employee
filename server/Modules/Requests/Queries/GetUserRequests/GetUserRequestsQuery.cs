using MediatR;
using server.Modules.Requests.Dto;

namespace server.Modules.Requests.Queries.GetUserRequests
{
    public record GetUserRequestsQuery() : IRequest<List<GetUserRequestDto>>;
}