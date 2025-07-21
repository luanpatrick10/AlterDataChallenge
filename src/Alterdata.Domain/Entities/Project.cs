using Shared.Entities;
using Shared.Validations;

namespace Alterdata.Domain.Entities;

public class Project : AggregateRoot
{
    public Project(string name,string description): base(Guid.NewGuid()) 
    {
        Name = name;
        Description = description;
        Validate();
    }

    public string Name { get; }
    public string Description { get; }

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
}