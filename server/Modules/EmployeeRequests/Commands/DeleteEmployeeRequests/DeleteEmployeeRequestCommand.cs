using MediatR;
using server.Modules.Common.Responses;

namespace server.Modules.EmployeeRequests.Commands.DeleteEmployeeRequest
{
    public record DeleteEmployeeRequestCommand(int EmployeeRequestId) : IRequest<CommandResponse<string>>;
}