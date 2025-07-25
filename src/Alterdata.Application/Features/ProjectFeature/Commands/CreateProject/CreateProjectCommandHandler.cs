using Alterdata.Domain.Repositories;
using Alterdata.Domain.Entities;
using Shared.Exceptions;
using Shared.Mediator;

namespace Alterdata.Application.Features.ProjectFeature.Commands.CreateProject;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Guid>
{
    private readonly IProjectRepository _projectRepository;

    public CreateProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Guid> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var exists = await _projectRepository.ExistsByNameAndDescriptionAsync(request.Name, request.Description);
        if (exists)
            throw new BusinessRuleException("A project with the same name and description already exists.");

        var project = new Project(request.Name, request.Description);
        try
        {
            await _projectRepository.BeginTransactionAsync();
            await _projectRepository.AddAsync(project);
            await _projectRepository.CommitTransactionAsync(project);
        }
        catch (Exception)
        {
            await _projectRepository.RollbackTransactionAsync();
            throw;
        }
        return project.Id;
    }
}
