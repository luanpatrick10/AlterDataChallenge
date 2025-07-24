using Shared.Entities;
using Shared.Validations;

namespace Alterdata.Domain.Entities;

public class TaskSpentTime : Entity
{
    public TaskSpentTime(DateTime started,DateTime finishedAt) : base(Guid.NewGuid())
    {
        StartedAt = started;
        FinishedAt = finishedAt;
        Validate();
    }
    
    public TaskSpentTime() : base(Guid.NewGuid())
    {
    }

    public DateTime StartedAt { get; private set; }
    public DateTime FinishedAt { get; private set; }
    public Guid TaskId { get; private set; }
    public Task Task { get; set; }
    public sealed override void Validate()
    {
    }
    
    public void ValidateStartedAt()
    {
        Validations.DateIsNotNull(StartedAt);
    }
    
    public void ValidateFinishedAt()
    {
        Validations.DateIsNotNull(FinishedAt);
        Validations.DateIsGreaterThan(FinishedAt, StartedAt);
    }
    
    
    public void SetSpentTime(DateTime startedAt, DateTime finishedAt,TimeSpan differenceLimit
    )
    {
        Validations.DateIsNotNull(startedAt);
        Validations.DateIsNotNull(FinishedAt);
        Validations.IsNotNull(differenceLimit);
        Validations.TimeIsNotNegative(differenceLimit);
        ValidateSpentTime();
        StartedAt = startedAt;
        FinishedAt = finishedAt;
    }
    
    public void ValidateSpentTime()
    {
        if (FinishedAt <= StartedAt)
            throw new ArgumentException("FinishedAt must be greater than StartedAt", nameof(FinishedAt));
    }
}