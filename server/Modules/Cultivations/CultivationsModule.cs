using Microsoft.AspNetCore.Authorization;
using MediatR;
using MongoDB.Driver;
using server.Data.Entities;
using server.Modules.Cultivations.Dto;
using server.Modules.Cultivations.Commands;
using Microsoft.AspNetCore.Mvc;

namespace server.Modules.Cultivations
{
    public class CultivationsModule : IModule
    {
        public const string BasePath = "api/cultivations";
        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints, IConfiguration config)
        {
            // get all cultivations 
            endpoints.MapGet(BasePath + "/list", [AllowAnonymous] async (DataContext context, CancellationToken token) =>
            {
                var cultivations = await context.Cultivations.ToListAsync(token);

                if (cultivations == null)
                    return Results.BadRequest();

                return Results.Ok(cultivations);
            })
            .WithName("GetCultivations")
            .WithTags("Cultivation")
            .Produces<List<Cultivation>>(200)
            .Produces(400)
            .Produces(401)
            .Produces(404)
            .Produces(500);

            endpoints.MapGet(BasePath + "/all",
            [AllowAnonymous]
            async ([FromBody] GetAllDto dto, IMediator mediator, CancellationToken token)
            => Results.Ok(await mediator.Send(new GetAllCommand(dto), token))
            )
            .WithName("GetCultivationsAll")
            .WithTags("Cultivation")
            .Produces<List<Location>>(200)
            .Produces(400)
            .Produces(401)
            .Produces(404)
            .Produces(500);

            endpoints.MapGet(BasePath + "/{id}",
            [Authorize]
            async (int id, IMediator mediator, CancellationToken token)
            => Results.Ok(await mediator.Send(new GetByIdCommand(id), token))
            )
            .WithName("GetCultivationById")
            .WithTags("Cultivation")
            .Produces<List<Location>>(200)
            .Produces(400)
            .Produces(401)
            .Produces(404)
            .Produces(500);

            return endpoints;
        }

    }
}