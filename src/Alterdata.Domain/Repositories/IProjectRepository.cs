using Alterdata.Domain.Entities;
using Shared.Repositories;

namespace Alterdata.Domain.Repositories;

public interface IProjectRepository : IBaseRepository<Project>
{
    Task<bool> ExistsByNameAndDescriptionAsync(string name, string description);
    Task<Project?> GetProjectWithTasksOrDefaultAsync(Guid projectId);
    Task<Guid> AddTaskAsync(Domain.Entities.Task task);
}