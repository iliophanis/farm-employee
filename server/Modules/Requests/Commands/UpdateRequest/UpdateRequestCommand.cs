using MediatR;
using server.Modules.Common.Responses;
using server.Modules.Requests.Dto;

namespace server.Modules.Requests.Commands.UpdateRequest
{
    public record UpdateRequestCommand(UpdateRequestDto UpdateRequestDto) : IRequest<CommandResponse<string>>;
}