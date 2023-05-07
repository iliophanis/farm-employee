using MediatR;
using server.Modules.Cultivations.Dto;

namespace server.Modules.Cultivations.Commands
{
    public record GetAllCommand(GetAllDto GetAllDto) : IRequest<GetAllResponseDto>;
}