using Shared.Entities;

namespace Shared.Repositories;

public interface IBaseRepository<T> : IUnitOfWorker<T> where T : AggregateRoot
{
    Task<T> GetByIdAsync(Guid id) ;
    T GetById(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<bool> ExistsAsync(Guid id);
    Task AddAsync(T entity);
}