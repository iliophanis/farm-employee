using MediatR;
using server.Modules.Requests.Dto;

namespace server.Modules.Requests.Commands.DeleteRequest
{
    public record DeleteRequestCommand(DeleteRequestDto DeleteRequestDto) : IRequest<DeleteRequestResponseDto>;
}