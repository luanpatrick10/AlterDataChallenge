using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Shared.Events;
using Alterdata.Domain.Entities;

namespace Alterdata.Infra.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<Event>();
        modelBuilder.Ignore<IEnumerable<IEvent>>();        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);        
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Project> Projects { get; set; }
    public DbSet<Alterdata.Domain.Entities.Task> Tasks { get; set; }
    public DbSet<TaskComment> TaskComments { get; set; }
    public DbSet<TaskSpentTime> TaskSpentTimes { get; set; }
}