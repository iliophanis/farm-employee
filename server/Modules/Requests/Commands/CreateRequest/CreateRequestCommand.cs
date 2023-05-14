using MediatR;
using server.Modules.Requests.Dto;

namespace server.Modules.Requests.Commands.CreateRequest
{
    public record CreateRequestCommand(CreateRequestDto CreateRequestDto) : IRequest<CreateRequestResponseDto>;
}