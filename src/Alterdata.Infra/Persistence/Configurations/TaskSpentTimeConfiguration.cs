using Alterdata.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AlterDataTask = Alterdata.Domain.Entities.Task;

namespace Alterdata.Infra.Persistence.Configurations;

public class TaskSpentTimeConfiguration : IEntityTypeConfiguration<TaskSpentTime>
{
    public void Configure(EntityTypeBuilder<TaskSpentTime> builder)
    {
        builder.ToTable("TaskSpentTimes");
        builder.HasKey(tst => tst.Id);
        builder.Property(tst => tst.StartedAt).IsRequired();
        builder.Property(tst => tst.FinishedAt).IsRequired();

        builder.HasOne<AlterDataTask>()
            .WithMany(t => t.SpentTimes)
            .HasForeignKey(tst => tst.TaskId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
