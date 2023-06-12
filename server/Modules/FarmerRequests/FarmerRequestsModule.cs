using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Modules.EmployeeRequests.Queries.GetFarmerRequests;

namespace server.Modules.FarmerRequests
{
    public class FarmerRequestsModule : IModule
    {
        public const string BasePath = "api/farmerRequests";
        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints, IConfiguration config)
        {

            endpoints.MapGet(
            BasePath + "/list",
            [Authorize(Roles = "Farmer")]
            async ([FromQuery] int PageSize, [FromQuery] int CurrentPage, [FromQuery] string? Filter, [FromQuery] string Type, IMediator mediator, CancellationToken token)
            => Results.Ok(await mediator.Send(new GetFarmerRequestsQuery(PageSize, CurrentPage, Filter, Type), token)))
            .WithName("GetFarmerRequestsQuery")
            .WithTags("FarmerRequest")
            .Produces<GetFarmerRequestsResponseDto>(200)
            .Produces(400)
            .Produces(401)
            .Produces(404)
            .Produces(500);

            return endpoints;
        }

    }
}
