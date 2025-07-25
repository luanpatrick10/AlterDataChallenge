using Shared.Entities;

namespace Shared.Repositories;

public interface IUnitOfWorker<TEntity> where TEntity : AggregateRoot
{
    Task BeginTransactionAsync();
    Task CommitTransactionAsync(TEntity? entity = null);
    Task RollbackTransactionAsync();
    Task SaveChangesAsync(TEntity? entity = null);
}