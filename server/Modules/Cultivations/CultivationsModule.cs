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
            .Produces<List<Cultivation>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden);

            endpoints.MapGet(BasePath + "/getall", 
            [AllowAnonymous] 
            async ([FromBody] GetAllDto dto, IMediator mediator, CancellationToken token)
            =>Results.Ok(await mediator.Send(new GetAllCommand(dto), token))
            )
            .Produces<List<Location>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden);
            
            return endpoints;
        }

    }
}