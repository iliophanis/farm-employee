using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Modules.Users.Commands.GoogleAuth;
using server.Modules.Users.Dto;

namespace server.Modules.Users
{
    public class UsersModule : IModule
    {
        public const string BasePath = "api/users";
        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints, IConfiguration config)
        {
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

            return endpoints;
        }

    }
}
