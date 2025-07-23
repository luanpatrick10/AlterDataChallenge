using Alterdata.Domain.Entities;
using Shared.Repositories;

namespace Alterdata.Domain.Repositories;

public interface IProjectRepository : IBaseRepository<Project>
{
    Task<bool> ExistsByNameAndDescriptionAsync(string name, string description);
}