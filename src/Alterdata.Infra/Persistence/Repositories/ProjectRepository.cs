using Alterdata.Domain.Entities;
using Alterdata.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared.Repositories;

namespace Alterdata.Infra.Persistence.Repositories;

public class ProjectRepository : BaseRepository<Project>,IProjectRepository
{
    public ProjectRepository(DbContext dbContext) : base(dbContext)
    {
    }
}