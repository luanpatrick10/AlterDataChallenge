using Alterdata.Domain.Entities;
using Alterdata.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared.Repositories;

namespace Alterdata.Infra.Persistence.Repositories;

public class ProjectRepository : BaseRepository<Project>, IProjectRepository
{
    private readonly DbSet<Domain.Entities.Task> _taskRepository;
    public ProjectRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _taskRepository = dbContext.Set<Domain.Entities.Task>();
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
}