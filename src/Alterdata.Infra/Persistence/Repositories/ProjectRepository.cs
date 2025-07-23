using Alterdata.Domain.Entities;
using Alterdata.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared.Repositories;

namespace Alterdata.Infra.Persistence.Repositories;

public class ProjectRepository : BaseRepository<Project>, IProjectRepository
{
    public ProjectRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> ExistsByNameAndDescriptionAsync(string name, string description)
    {
        return await _entityRepository.AnyAsync(p => p.Name == name && p.Description == description);
    }
}