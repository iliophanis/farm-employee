using MediatR;
using server.Modules.Common.Responses;
using server.Modules.EmployeeRequests.Dto;

namespace server.Modules.EmployeeRequests.Commands.UpdateEmployeeRequest
{
    public record UpdateEmployeeRequestCommand (UpdateEmployeeRequestDto UpdateEmployeeRequestDto) : IRequest<CommandResponse<string>>;
}