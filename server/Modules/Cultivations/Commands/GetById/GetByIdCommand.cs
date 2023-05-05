using MediatR;
using server.Modules.Cultivations.Dto;

namespace server.Modules.Cultivations.Commands
{
    public record GetByIdCommand(int id) : IRequest<GetByIdResponseDto>;
}