using Alterdata.Domain.Entities;
using Alterdata.Domain.Repositories;
using Shared.Mediator;

namespace Alterdata.Application.Features.Project.Commands.CreateProject;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Guid>
{
    private readonly IProjectRepository _projectRepository;

    public CreateProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Guid> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = new Domain.Entities.Project(request.Name, request.Description);
        try
        {
            await _projectRepository.BeginTransactionAsync();
            await _projectRepository.AddAsync(project);
            await _projectRepository.CommitTransactionAsync();
        }
        catch (Exception)
        {
            await _projectRepository.RollbackTransactionAsync();
            throw;
        }
        return project.Id;
    }
}
