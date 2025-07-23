using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Shared.Events;
using Alterdata.Domain.Entities;

namespace Alterdata.Infra.Persistence;


public class ApplicationDbContext : DbContext
{
    private readonly string? _inMemoryDbName;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public ApplicationDbContext(string inMemoryDbName)
    {
        _inMemoryDbName = inMemoryDbName;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured && !string.IsNullOrEmpty(_inMemoryDbName))
        {
            optionsBuilder.UseInMemoryDatabase(_inMemoryDbName);
        }
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<Event>();
        modelBuilder.Ignore<IEnumerable<IEvent>>();        
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(ApplicationDbContext))!);
        base.OnModelCreating(modelBuilder);
    }
}