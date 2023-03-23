using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using MongoDB.Driver;

using server.Data;
using server.Data.Entities;

namespace server.Modules.Users
{
    public class UsersModule : IModule
    {
        public const string BasePath = "api/users";
        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints, IConfiguration config)
        {
            // get all users
            endpoints.MapGet(BasePath + "/users", [Authorize] async (DataContext context, CancellationToken token) =>
            {
                var users = await context.Cultivations.ToListAsync(token);

                if (users == null)
                    return Results.BadRequest();

                return Results.Ok(users);
            })
            .Produces<List<Cultivation>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden);

            return endpoints;
        }

    }
}
