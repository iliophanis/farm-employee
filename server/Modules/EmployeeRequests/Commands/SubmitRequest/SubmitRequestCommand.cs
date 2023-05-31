using MediatR;
using server.Modules.Common.Responses;
using server.Modules.EmployeeRequests.Dto;

namespace server.Modules.EmployeeRequests.Commands.SubmitRequest
{
    public record SubmitRequestCommand (SubmitRequestDto SubmitRequestDto) : IRequest<CommandResponse<string>>;
}