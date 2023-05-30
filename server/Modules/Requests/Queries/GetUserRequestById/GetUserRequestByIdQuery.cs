using MediatR;
using server.Modules.Requests.Dto;

namespace server.Modules.Requests.Queries.GetUserRequestById
{
    public record GetUserRequestByIdQuery(int RequestId) : IRequest<GetUserRequestByIdDto>;
}