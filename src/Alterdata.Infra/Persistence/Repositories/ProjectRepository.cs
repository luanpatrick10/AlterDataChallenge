using Alterdata.Domain.Entities;
using Alterdata.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared.Mediator;
using Shared.Repositories;


namespace Alterdata.Infra.Persistence.Repositories;

public class ProjectRepository : BaseRepository<Project>, IProjectRepository
{
    private readonly DbSet<Domain.Entities.Task> _taskRepository;
    private readonly DbSet<TaskComment> _taskCommentRepository;
    private readonly DbSet<TaskSpentTime> _taskSpentTimeRepository;
    private readonly ApplicationDbContext _applicationDbContext;
    public ProjectRepository(ApplicationDbContext dbContext, AppMediator appMediator) : base(dbContext, appMediator)
    {
        _taskRepository = dbContext.Set<Domain.Entities.Task>();
        _taskCommentRepository = dbContext.Set<TaskComment>();
        _taskSpentTimeRepository = dbContext.Set<TaskSpentTime>();
        _applicationDbContext = dbContext;
    }

    public async Task<bool> ExistsByNameAndDescriptionAsync(string name, string description)
    {
        return await _entityRepository.AnyAsync(p => p.Name == name && p.Description == description);
    }

    public async Task<Project?> GetProjectWithTasksOrDefaultAsync(Guid projectId)
    {
        return await _entityRepository
            .Include(p => p.Tasks)
            .FirstOrDefaultAsync(p => p.Id == projectId);
    }    

    public async Task<Guid> AddTaskAsync(Domain.Entities.Task task)
    {
        await _taskRepository.AddAsync(task);
        await SaveChangesAsync();
        return task.Id;
    }
    public async Task<Domain.Entities.Task?> GetTaskDetailsByIdAsync(Guid taskId)
    {
        return await _taskRepository
            .Include(t => t.TasksComment)
            .Include(t => t.SpentTimes)
            .FirstOrDefaultAsync(t => t.Id == taskId);
    }

    public async Task<Guid> AddTaskCommentAsync(TaskComment comment)
    {
        await _taskCommentRepository.AddAsync(comment);
        await _applicationDbContext.SaveChangesAsync();
        return comment.Id;
    }
    public async Task<Guid> AddTaskSpentTimeAsync(TaskSpentTime spentTime)
    {
        await _taskSpentTimeRepository.AddAsync(spentTime);
        await _applicationDbContext.SaveChangesAsync();
        return spentTime.Id;
    }
}