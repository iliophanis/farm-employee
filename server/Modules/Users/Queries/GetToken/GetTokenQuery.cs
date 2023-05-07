using MediatR;
using server.Modules.Users.Dto;
namespace server.Modules.Users.Queries.GetToken
{
    public record GetTokenQuery(string UserName, string AuthProvider) : IRequest<GetTokenResponseDto>;
}