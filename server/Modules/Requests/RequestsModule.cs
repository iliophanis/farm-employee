using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Modules.Requests.Dto;
using server.Modules.Requests.Queries.GetUserRequests;
using server.Modules.Requests.Commands.CreateRequest;
using server.Modules.Requests.Commands.UpdateRequest;
using server.Modules.Requests.Commands.DeleteRequest;
using server.Modules.Common.Responses;
using server.Modules.Requests.Queries.GetUserRequestById;

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
            async ([FromQuery] decimal? MinLat, [FromQuery] decimal? MaxLat, [FromQuery] decimal? MinLon, [FromQuery] decimal? MaxLon, IMediator mediator, CancellationToken token)
            => Results.Ok(await mediator.Send(new GetUserRequestsQuery(MinLat, MaxLat, MinLon, MaxLon), token)))
            .WithName("GetUserRequestsQuery")
            .WithTags("Requests")
            .Produces<List<GetUserRequestDto>>(200)
            .Produces(400)
            .Produces(401)
            .Produces(404)
            .Produces(500);

            // endpoints.MapGet(
            // BasePath + "/user/authorized",
            // [Authorize]
            // async (IMediator mediator, CancellationToken token)
            // => Results.Ok(await mediator.Send(new GetAuthorizedUserRequestsQuery(), token)))
            // .WithName("GetAuthorizedUserRequestsQuery")
            // .WithTags("Requests")
            // .Produces<List<Request>>(200)
            // .Produces(400)
            // .Produces(401)
            // .Produces(404)
            // .Produces(500);

            endpoints.MapGet(
            BasePath + "/user/{id}",
            [Authorize]
            async (int id, IMediator mediator, CancellationToken token)
            => Results.Ok(await mediator.Send(new GetUserRequestByIdQuery(id), token)))
            .WithName("GetUserRequestByIdQuery")
            .WithTags("Requests")
            .Produces<GetUserRequestByIdDto>(200)
            .Produces(400)
            .Produces(401)
            .Produces(404)
            .Produces(500);

            endpoints.MapPost(
            BasePath + "",
            [Authorize(Roles = "Farmer, Admin")]
            async ([FromBody] CreateRequestDto dto, IMediator mediator, CancellationToken token)
            => Results.Ok(await mediator.Send(new CreateRequestCommand(dto), token)))
            .WithName("CreateRequestCommand")
            .WithTags("Requests")
            .Produces<CommandResponse<string>>(200)
            .Produces(400)
            .Produces(401)
            .Produces(404)
            .Produces(500);

            endpoints.MapPut(
            BasePath + "",
            [Authorize(Roles = "Farmer, Admin")]
            async ([FromBody] UpdateRequestDto dto, IMediator mediator, CancellationToken token)
            => Results.Ok(await mediator.Send(new UpdateRequestCommand(dto), token)))
            .WithName("UpdateRequestCommand")
            .WithTags("Requests")
            .Produces<CommandResponse<string>>(200)
            .Produces(400)
            .Produces(401)
            .Produces(404)
            .Produces(500);

            endpoints.MapDelete(
            BasePath + "",
            [Authorize(Roles = "Farmer")]
            async ([FromBody] DeleteRequestDto dto, IMediator mediator, CancellationToken token)
            => Results.Ok(await mediator.Send(new DeleteRequestCommand(dto), token)))
            .WithName("DeleteRequestCommand")
            .WithTags("Requests")
            .Produces<CommandResponse<string>>(200)
            .Produces(400)
            .Produces(401)
            .Produces(404)
            .Produces(500);

            return endpoints;
        }

    }
}
