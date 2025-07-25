using Shared.Entities;
using Shared.Exceptions;
using Shared.Validations;

namespace Alterdata.Domain.Entities;

public class TaskSpentTime : Entity, ILimitTimeSpentStrategy
{
    private const double MaxAllowedHours = 8;
    public TaskSpentTime(DateTime started, DateTime finishedAt, Guid taskId) : base(Guid.NewGuid())
    {
        StartedAt = started;
        FinishedAt = finishedAt;
        TaskId = taskId;
        Validate();
    }
    
    public TaskSpentTime() : base(Guid.NewGuid())
    {
    }

    public DateTime StartedAt { get; private set; }
    public DateTime FinishedAt { get; private set; }
    public Guid TaskId { get; private set; }
    public Task Task { get; private set; }
    public sealed override void Validate()
    {
        ValidateStartedAt();
        ValidateFinishedAt();
        ValidateSpendTimeLimit();
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
        ValidateSpendTimeLimit();
        StartedAt = startedAt;
        FinishedAt = finishedAt;
    }

public void ValidateSpendTimeLimit()
{
    if (FinishedAt <= StartedAt)
        throw new DomainException("FinishedAt must be greater than StartedAt");
    var spentHours = (FinishedAt - StartedAt).TotalHours;
    if (spentHours > MaxAllowedHours)
        throw new DomainException($"Spent time cannot exceed {MaxAllowedHours} hours");
}
}