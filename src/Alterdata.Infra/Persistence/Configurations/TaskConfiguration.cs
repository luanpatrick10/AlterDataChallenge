using Alterdata.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AlterDataTask = Alterdata.Domain.Entities.Task;

namespace Alterdata.Infra.Persistence.Configurations;

public class TaskConfiguration : IEntityTypeConfiguration<AlterDataTask>
{
    public void Configure(EntityTypeBuilder<AlterDataTask> builder)
    {
        builder.ToTable("Tasks");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Title).IsRequired().HasMaxLength(200);
        builder.Property(t => t.Description).IsRequired().HasMaxLength(1000);
        builder.Property(t => t.DueDate).IsRequired();
        builder.Property(t => t.Status).IsRequired();

        builder.HasOne(t => t.Project)
            .WithMany()
            .HasForeignKey(t => t.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.TasksComment)
            .WithOne(tc => tc.Task)
            .HasForeignKey(tc => tc.TaskId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.SpentTimes)
            .WithOne()
            .HasForeignKey(st => st.TaskId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
