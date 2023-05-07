using MediatR;
using server.Modules.Common.Responses;
using server.Modules.Users.Dto;

namespace server.Modules.Users.Commands.AddRole
{
    public record AddRoleCommand(AddRoleDto AddRoleDto) : IRequest<CommandResponse<string>>;
}