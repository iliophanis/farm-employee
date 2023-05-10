using MediatR;
using Microsoft.AspNetCore.Authorization;
using server.Modules.Requests.Dto;
using server.Modules.Requests.Queries.GetUserRequests;

namespace server.Modules.Requests
{
    public class RequestsModule : IModule
    {
        public const string BasePath = "api/requests";
        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints, IConfiguration config)
        {
            endpoints.MapGet(
            BasePath + "/user",
            [AllowAnonymous]
            async (IMediator mediator, CancellationToken token)
            => Results.Ok(await mediator.Send(new GetUserRequestsQuery(), token)))
            .WithName("GetUserRequestsQuery")
            .WithTags("Requests")
            .Produces<List<GetUserRequestDto>>(200)
            .Produces(400)
            .Produces(401)
            .Produces(404)
            .Produces(500);

            return endpoints;
        }

    }
}
