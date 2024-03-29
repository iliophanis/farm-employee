using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Data.Entities;
using server.Modules.EmployeeRequests.Dto;
using server.Modules.EmployeeRequests.Commands.SubmitRequest;
using server.Modules.EmployeeRequests.Commands.UpdateEmployeeRequest;
using server.Modules.EmployeeRequests.Commands.DeleteEmployeeRequest;
using server.Modules.EmployeeRequests.Queries.GetByIdEmployeeRequest;
using server.Modules.EmployeeRequests.Queries.GetListEmployeeRequest;
using server.Modules.Common.Responses;
using server.Modules.EmployeeRequests.Queries.GetAllEmployeeRequests;

namespace server.Modules.Requests
{
    public class EmployeeRequestsModule : IModule
    {
        public const string BasePath = "api/employeeRequests";
        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints, IConfiguration config)
        {
            endpoints.MapGet(
            BasePath + "/employee",
            [AllowAnonymous]
            async (int employeeRequestId, IMediator mediator, CancellationToken token)
            => Results.Ok(await mediator.Send(new GetByIdEmployeeRequestQuery(employeeRequestId), token)))
            .WithName("GetByIdEmployeeRequestQuery")
            .WithTags("EmployeeRequest")
            .Produces<List<GetByIdEmployeeRequestDto>>(200)
            .Produces(400)
            .Produces(401)
            .Produces(404)
            .Produces(500);

            endpoints.MapGet(
            BasePath + "/employee/all",
            [AllowAnonymous]
            async (int requestId, IMediator mediator, CancellationToken token)
            => Results.Ok(await mediator.Send(new GetListEmployeeRequestQuery(requestId), token)))
            .WithName("GetListEmployeeRequestQuery")
            .WithTags("EmployeeRequest")
            .Produces<List<Request>>(200)
            .Produces(400)
            .Produces(401)
            .Produces(404)
            .Produces(500);

            endpoints.MapGet(
            BasePath + "/list",
            [Authorize(Roles = "Employee")]
            async ([FromQuery] int PageSize, [FromQuery] int CurrentPage, [FromQuery] string? Filter, [FromQuery] string Type, IMediator mediator, CancellationToken token)
            => Results.Ok(await mediator.Send(new GetAllEmployeeRequestsQuery(PageSize, CurrentPage, Filter, Type), token)))
            .WithName("GetAllEmployeeRequestsQuery")
            .WithTags("EmployeeRequest")
            .Produces<GetAllEmployeeRequestsResponseDto>(200)
            .Produces(400)
            .Produces(401)
            .Produces(404)
            .Produces(500);

            endpoints.MapPost(
            BasePath + "",
            [Authorize(Roles = "Employee, Admin")]
            async ([FromBody] SubmitRequestDto dto, IMediator mediator, CancellationToken token)
            => Results.Ok(await mediator.Send(new SubmitRequestCommand(dto), token)))
            .WithName("CreateEmployeeRequestCommand")
            .WithTags("EmployeeRequest")
            .Produces<CommandResponse<string>>(200)
            .Produces(400)
            .Produces(401)
            .Produces(404)
            .Produces(500);

            endpoints.MapPut(
            BasePath + "",
            [Authorize(Roles = "Employee")]
            async ([FromBody] UpdateEmployeeRequestDto dto, IMediator mediator, CancellationToken token)
            => Results.Ok(await mediator.Send(new UpdateEmployeeRequestCommand(dto), token)))
            .WithName("UpdateEmployeeRequestCommand")
            .WithTags("EmployeeRequest")
            .Produces<CommandResponse<string>>(200)
            .Produces(400)
            .Produces(401)
            .Produces(404)
            .Produces(500);

            endpoints.MapDelete(
            BasePath + "/{id}",
            [Authorize(Roles = "Employee")]
            async (int id, IMediator mediator, CancellationToken token)
            => Results.Ok(await mediator.Send(new DeleteEmployeeRequestCommand(id), token)))
            .WithName("DeleteEmployeeRequestCommand")
            .WithTags("EmployeeRequest")
            .Produces<CommandResponse<string>>(200)
            .Produces(400)
            .Produces(401)
            .Produces(404)
            .Produces(500);

            return endpoints;
        }

    }
}
