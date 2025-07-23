using Alterdata.Application.Features.Project.Commands.CreateProject;
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
    } 
}