using Alterdata.Application.Features.ProjectFeature.Commands.CreateProject;
using Alterdata.Application.Features.ProjectFeature.Commands.AddTask;
using Alterdata.Application.Features.ProjectFeature.Queries.GetProject;
using Shared.Mediator;

namespace Alterdata.RestAPI.Project;

public static class ProjectEndpoints
{

    public static void AddProjectEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/projects", async (CreateProjectCommand command, AppMediator mediator) =>
        {
            var id = await mediator.Send(command);
            return Results.Created($"/api/projects/{id}", id);
        });

        app.MapPost("/api/projects/{projectId}/tasks", async (Guid projectId, AddTaskCommand command, AppMediator mediator) =>
        {
            command.ProjectId = projectId;
            var id = await mediator.Send(command);
            return Results.Created($"/api/projects/{projectId}/tasks/{id}", id);
        });
    }

    public static void AddGetProjectEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/projects/{id}", async (Guid id, AppMediator mediator) =>
        {
            var result = await mediator.Send<GetProjectDto>(new GetProjectQuery(id));
            return result is not null ? Results.Ok(result) : Results.NotFound();
        });
    }
}