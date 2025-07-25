using Alterdata.Domain.Entities;
using Shared.Repositories;
using Task = Alterdata.Domain.Entities.Task;

namespace Alterdata.Domain.Repositories;

public interface IProjectRepository : IBaseRepository<Project>
{
    Task<bool> ExistsByNameAndDescriptionAsync(string name, string description);
    Task<Project?> GetProjectWithTasksOrDefaultAsync(Guid projectId);
    Task<Guid> AddTaskAsync(Task task);
    Task<Guid> AddTaskCommentAsync(TaskComment comment);
    Task<Task?> GetTaskDetailsByIdAsync(Guid taskId);
    Task<Guid> AddTaskSpentTimeAsync(TaskSpentTime spentTime);
}