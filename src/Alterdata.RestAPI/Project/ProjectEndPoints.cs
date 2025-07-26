using Alterdata.Application.Features.ProjectFeature.Commands.CreateProject;
using Alterdata.Application.Features.ProjectFeature.Commands.AddTask;
using Alterdata.Application.Features.ProjectFeature.Commands.CreateTaskComment;
using Alterdata.Application.Features.ProjectFeature.Commands.RegisterTaskSpendTime;
using Alterdata.Application.Features.ProjectFeature.Commands;
using Alterdata.Application.Features.ProjectFeature.Queries.GetProject;
using Shared.Mediator;
using Alterdata.Application.Features.ProjectFeature.Queries;
using System.Net;

namespace Alterdata.RestAPI.Project;

public static class ProjectEndpoints
{

    public static void AddProjectEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/projects", async (CreateProjectCommand command, AppMediator mediator) =>
        {
            var id = await mediator.Send(command);
            return Results.Created($"/api/projects/{id}", id);
        }).AddEndpointFilter(new RoleFilter("Manager"));

        app.MapPost("/api/projects/{projectId}/tasks", async (Guid projectId, AddTaskCommand command, AppMediator mediator) =>
        {
            command.ProjectId = projectId;
            var id = await mediator.Send(command);
            return Results.Created($"/api/projects/{projectId}/tasks/{id}", id);
        }).AddEndpointFilter(new RoleFilter("Manager"));

        app.MapPut("/api/projects/tasks/{taskId}/status", async (Guid taskId, ChangeTaskStatusCommand command, AppMediator mediator) =>
        {
            command.TaskId = taskId;
            var result = await mediator.Send(command);
            return result ? Results.Ok() : Results.NotFound();
        }).AddEndpointFilter(new RoleFilter("Member"));

        app.MapPost("/api/projects/tasks/{taskId}/comments", async (Guid taskId, CreateTaskCommentCommand command, AppMediator mediator) =>
        {
            command.SetTaskId(taskId);
            var id = await mediator.Send(command);
            return Results.Created($"/api/tasks/{taskId}/comments/{id}", id);
        }).AddEndpointFilter(new RoleFilter("Member"));

        app.MapPost("/api/projects/tasks/{taskId}/spent-time", async (Guid taskId, RegisterTaskSpendTimeCommand command, AppMediator mediator) =>
        {
            command.SetTaskId(taskId);
            var id = await mediator.Send(command);
            return Results.Created($"/api/tasks/{taskId}/spent-time/{id}", id);
        }).AddEndpointFilter(new RoleFilter("Member"));

        app.MapGet("/api/projects/{id}", async (Guid id, AppMediator mediator) =>
        {
            var result = await mediator.Send(new GetProjectQuery(id));
            return result is not null ? Results.Ok(result) : Results.NotFound();
        }).AddEndpointFilter(new RoleFilter("Member"));

        app.MapGet("/api/projects/tasks/{taskId}", async (Guid taskId, AppMediator mediator) =>
        {
            var result = await mediator.Send(new GetTaskDetailsQuery(taskId));
            return result is not null ? Results.Ok(result) : Results.NotFound();
        }).AddEndpointFilter(new RoleFilter("Member"));
    }    
}