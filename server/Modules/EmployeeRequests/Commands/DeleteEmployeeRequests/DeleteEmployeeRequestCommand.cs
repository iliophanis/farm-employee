using MediatR;
using server.Modules.Common.Responses;
using server.Modules.EmployeeRequests.Dto;

namespace server.Modules.EmployeeRequests.Commands.DeleteEmployeeRequest
{
    public record DeleteEmployeeRequestCommand (DeleteEmployeeRequestDto DeleteEmployeeRequestDto) : IRequest<CommandResponse<string>>;
}