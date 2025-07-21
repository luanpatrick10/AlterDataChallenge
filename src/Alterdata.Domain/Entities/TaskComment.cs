using Shared.Entities;
using AlterDataTask = Alterdata.Domain.Entities.Task;

namespace Alterdata.Domain.Entities;

public class TaskComment : Entity
{
    public TaskComment(string text) : base(Guid.NewGuid())
    {
        Text = text;
        CreateAt = DateTime.Now;
        Validate();
    }

    public string Text { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime? UpdateAt { get; set; }
    
    //Ef Relations
    public AlterDataTask Task { get;}
    public Guid TaskId { get;}

    public sealed override void Validate()
    {
    }
}