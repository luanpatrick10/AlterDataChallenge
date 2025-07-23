

using Shared.Mediator;

namespace Alterdata.Application.Features.ProjectFeature.Commands.CreateProject;

public class CreateProjectCommand : IRequest<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; }
}
