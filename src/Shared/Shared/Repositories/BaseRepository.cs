using Microsoft.EntityFrameworkCore;
using Shared.Entities;
using Shared.Exceptions;

namespace Shared.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : AggregateRoot
{
    private readonly DbContext _dbContext;
    protected readonly DbSet<TEntity> _entityRepository;
    public BaseRepository(DbContext dbContext)
    {
        this._dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        this._entityRepository = this._dbContext.Set<TEntity>();

    }
    
    public async Task<TEntity> GetByIdAsync(Guid id)
    {
        return await _entityRepository.FindAsync(id) ?? throw new  NotFoundException();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _entityRepository.ToListAsync();
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        return _entityRepository.AnyAsync(e => e.Id.Equals(id));
    }

    public async Task AddAsync(TEntity entity)
    {
        entity.Validate();
        await _entityRepository.AddAsync(entity);
    }

    public async Task BeginTransactionAsync()
    {
        await _dbContext.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        await _dbContext.Database.RollbackTransactionAsync();
    }
}