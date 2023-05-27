using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Data.Entities;
using server.Modules.EmployeeRequests.Dto;
using server.Modules.EmployeeRequests.Commands.CreateEmployeeRequest;
using server.Modules.EmployeeRequests.Commands.UpdateEmployeeRequest;
using server.Modules.EmployeeRequests.Commands.DeleteEmployeeRequest;
using server.Modules.EmployeeRequests.Queries.GetByIdEmployeeRequest;
using server.Modules.EmployeeRequests.Queries.GetListEmployeeRequest;
using server.Modules.Common.Responses;

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
            async (int employeeRequestId, int employeeId, IMediator mediator, CancellationToken token)
            => Results.Ok(await mediator.Send(new GetByIdEmployeeRequestQuery(employeeRequestId, employeeId), token)))
            .WithName("GetByIdEmployeeRequestQuery")
            .WithTags("EmployeeRequest")
            .Produces<List<GetByIdEmployeeRequestResponseDto>>(200)
            .Produces(400)
            .Produces(401)
            .Produces(404)
            .Produces(500);

            endpoints.MapGet(
            BasePath + "/employee/all",
            [AllowAnonymous]
            async (int employeeRequestId, IMediator mediator, CancellationToken token)
            => Results.Ok(await mediator.Send(new GetListEmployeeRequestQuery(employeeRequestId), token)))
            .WithName("GetListEmployeeRequestQuery")
            .WithTags("EmployeeRequest")
            .Produces<List<Request>>(200)
            .Produces(400)
            .Produces(401)
            .Produces(404)
            .Produces(500);

            endpoints.MapPost(
            BasePath + "",
            [Authorize(Roles = "Employee, Admin")]
            async ([FromBody] CreateEmployeeRequestDto dto, IMediator mediator, CancellationToken token)
            => Results.Ok(await mediator.Send(new CreateEmployeeRequestCommand(dto), token)))
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
            BasePath + "",
            [Authorize(Roles = "Employee")]
            async ([FromBody] DeleteEmployeeRequestDto dto, IMediator mediator, CancellationToken token)
            => Results.Ok(await mediator.Send(new DeleteEmployeeRequestCommand(dto), token)))
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
