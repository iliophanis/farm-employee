using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Modules.Common.Responses;
using server.Modules.Users.Commands.AddRole;
using server.Modules.Users.Commands.GoogleAuth;
using server.Modules.Users.Dto;
using server.Modules.Users.Queries.GetToken;

namespace server.Modules.Users
{
    public class UsersModule : IModule
    {
        public const string BasePath = "api/users";
        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints, IConfiguration config)
        {
            endpoints.MapGet(
            BasePath + "/token/{userName}",
            [AllowAnonymous]
            async (string userName, [FromQuery] string authProvider, IMediator mediator, CancellationToken token)
            => Results.Ok(await mediator.Send(new GetTokenQuery(userName, authProvider), token)))
            .WithName("GetTokenQuery")
            .WithTags("User")
            .Produces<GetTokenResponseDto>(200)
            .Produces(400)
            .Produces(401)
            .Produces(404)
            .Produces(500);

            // google authenticate
            endpoints.MapPost(
            BasePath + "/google-auth",
            [AllowAnonymous]
            async ([FromBody] GoogleAuthDto dto, IMediator mediator, CancellationToken token)
            => Results.Ok(await mediator.Send(new GoogleAuthCommand(dto), token)))
            .WithName("GoogleAuth")
            .WithTags("User")
            .Produces<GoogleAuthResponseDto>(200)
            .Produces(400)
            .Produces(401)
            .Produces(404)
            .Produces(500);

            endpoints.MapPost(
            BasePath + "/role/",
            [AllowAnonymous]
            async ([FromBody] AddRoleDto dto, IMediator mediator, CancellationToken token)
            => Results.Ok(await mediator.Send(new AddRoleCommand(dto), token)))
            .WithName("AddRoleCommand")
            .WithTags("User")
            .Produces<CommandResponse<string>>(200)
            .Produces(400)
            .Produces(401)
            .Produces(404)
            .Produces(500);

            return endpoints;
        }

    }
}
