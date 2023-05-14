using MediatR;
using server.Data.Entities;

namespace server.Modules.Requests.Queries.GetAuthorizedUserRequests
{
    public record GetAuthorizedUserRequestsQuery() : IRequest<List<Request>>;
}