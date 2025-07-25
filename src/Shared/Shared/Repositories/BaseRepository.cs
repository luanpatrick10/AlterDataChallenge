using Microsoft.EntityFrameworkCore;
using Shared.Entities;
using Shared.Exceptions;
using Shared.Mediator;

namespace Shared.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : AggregateRoot
{
    private readonly DbContext _dbContext;
    protected readonly DbSet<TEntity> _entityRepository;
    private readonly AppMediator _appMediator;
    public BaseRepository(DbContext dbContext,AppMediator appMediator)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _entityRepository = _dbContext.Set<TEntity>();
        _appMediator = appMediator ?? throw new ArgumentNullException(nameof(appMediator));
    }
    
    public async Task<TEntity> GetByIdAsync(Guid id)
    {
        return await _entityRepository.FindAsync(id) ?? throw new  NotFoundException();
    }

    public TEntity GetById(Guid id)
    {
        return _entityRepository.Find(id) ?? throw new  NotFoundException();
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

    public async Task SaveChangesAsync(TEntity? entity = null)
    {        
        await _dbContext.SaveChangesAsync();
        await PublishEntityEvents(entity).ConfigureAwait(false);
    }

    public async Task BeginTransactionAsync()
    {
        await _dbContext.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync(TEntity? entity = null)
    {
        await _dbContext.SaveChangesAsync();
        await _dbContext.Database.CommitTransactionAsync();
        await PublishEntityEvents(entity).ConfigureAwait(false);
    }

    public async Task RollbackTransactionAsync()
    {
        await _dbContext.Database.RollbackTransactionAsync();
    }    

    private async Task PublishEntityEvents(TEntity? entity)
    {
        if (entity is { Events: not null } && entity.Events.Any())
        {
            foreach (var @event in entity.Events)
            {
                await _appMediator.Publish(@event, CancellationToken.None).ConfigureAwait(false);
            }
            entity.ClearEvents();
        }
    }
}