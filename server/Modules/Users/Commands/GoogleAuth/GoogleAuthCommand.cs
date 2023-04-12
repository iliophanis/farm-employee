using MediatR;
using server.Modules.Users.Dto;

namespace server.Modules.Users.Commands.GoogleAuth
{
    public record GoogleAuthCommand(GoogleAuthDto GoogleAuthDto) : IRequest<GoogleAuthResponseDto>;
}