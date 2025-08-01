using Shared.Entities;

namespace Alterdata.Domain.Entities;

public class TaskComment : Entity
{
    public TaskComment(string text, Guid taskId) : base(Guid.NewGuid())
    {
        Text = text;
        CreateAt = DateTime.Now;
        TaskId = taskId;
        Validate();
    }

    public string Text { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime? UpdateAt { get; set; }
    
    //Ef Relations
    public Task Task { get; }
    public Guid TaskId { get; }

    public sealed override void Validate()
    {
    }
}