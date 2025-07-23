using Alterdata.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AlterDataTask = Alterdata.Domain.Entities.Task;

namespace Alterdata.Infra.Persistence.Configurations;

public class TaskCommentConfiguration : IEntityTypeConfiguration<TaskComment>
{
    public void Configure(EntityTypeBuilder<TaskComment> builder)
    {
        builder.ToTable("TaskComments");
        builder.HasKey(tc => tc.Id);
        builder.Property(tc => tc.Text).IsRequired().HasMaxLength(1000);
        builder.Property(tc => tc.CreateAt).IsRequired();
        builder.Property(tc => tc.UpdateAt);

        builder.HasOne(tc => tc.Task)
            .WithMany(t => t.TasksComment)
            .HasForeignKey(tc => tc.TaskId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
