using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shared.Entities;
using Shared.Exceptions;
using Shared.Validations;
using TaskStatus = Alterdata.Domain.Enums.TaskStatus;

namespace Alterdata.Domain.Entities;

public class Task : Entity
{
    public Task(string title, string description, DateTime dueDate, Guid projectId) : base(Guid.NewGuid())
    {
        Title = title;
        Description = description;
        DueDate = dueDate;
        ProjectId = projectId;
        SetStatus(TaskStatus.Pendente);
        TasksComment = new List<TaskComment>();
        SpentTimes = new List<TaskSpentTime>();
        Validate();
    }
    
    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTime DueDate { get; private set; }
    public TaskStatus Status { get; private set; }
    
    
    //EF Relationships
    public Project Project { get; set; }
    public Guid ProjectId { get; set; }
    public ICollection<TaskComment> TasksComment { get; private set; }
    public ICollection<TaskSpentTime> SpentTimes { get; private set; }
    
    public sealed override void Validate()
    {
        ValidateTitle();
        ValidateDescription();
        ValidateDueDate();
    }
    
    public void AddTaskComment(TaskComment taskComment)
    {
        Validations.IsNotNull(taskComment);
        taskComment.Validate();
        ValidateTIfAfterDueDate();
        TasksComment ??= new List<TaskComment>();
        TasksComment.Add(taskComment);
    }

    private void ValidateTitle()
    {
        Validations.IsNotNullOrEmpty(Title);
    }
    
    private void ValidateDescription()
    {
        Validations.IsNotNullOrEmpty(Description);
    }

    private void ValidateDueDate()
    {
        Validations.DateIsNotNull(DueDate);
    }
    
    public void SetStatus(TaskStatus status)
    {
        if(!Enum.IsDefined(typeof(TaskStatus), status))
            throw new DomainException("Invalid task status");
        Status = status;
    }
    private void ValidateTIfAfterDueDate()
    {
        if (DueDate < DateTime.Now)
            throw new DomainException("Due date cannot be in the past");
    }

    private void ValidateIfTaskIsNotInProgress()
    {
        if(Status == TaskStatus.Concluído)
            throw new DomainException("Task is not in progress");
    }
    
    
    
}