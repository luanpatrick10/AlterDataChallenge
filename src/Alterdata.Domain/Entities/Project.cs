using Shared.Entities;
using Shared.Validations;

namespace Alterdata.Domain.Entities;

public class Project : AggregateRoot
{
    public Project(string name, string description) : base(Guid.NewGuid())
    {
        Name = name;
        Description = description;
        Validate();
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public ICollection<Task> Tasks { get; private set; } = new List<Task>();

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
        Validate();
    }

    public sealed override void Validate()
    {
        ValidateName();
        ValidateDescription();
    }

    private void ValidateName()
    {
        Validations.IsNotNullOrEmpty(Name);
    }

    private void ValidateDescription()
    {
        Validations.IsNotNullOrEmpty(Description);
    }

    public void AddTask(Task task)
    {
        task.Validate();
        Tasks.Add(task);
    }
}