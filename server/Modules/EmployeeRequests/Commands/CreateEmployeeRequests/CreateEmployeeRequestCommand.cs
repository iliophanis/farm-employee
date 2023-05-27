using MediatR;
using server.Modules.Common.Responses;
using server.Modules.EmployeeRequests.Dto;

namespace server.Modules.EmployeeRequests.Commands.CreateEmployeeRequest
{
    public record CreateEmployeeRequestCommand (CreateEmployeeRequestDto CreateEmployeeRequestDto) : IRequest<CommandResponse<string>>;
}