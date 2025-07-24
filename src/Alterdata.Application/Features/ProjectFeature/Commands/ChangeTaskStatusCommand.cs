using TaskStatus = Alterdata.Domain.Enums.TaskStatus;

namespace Alterdata.Application.Features.ProjectFeature.Commands
{
    public class ChangeTaskStatusCommand : Shared.Mediator.IRequest<bool>
    {
        public Guid TaskId { get; set; }
        public TaskStatus Status { get; set; }
        public ChangeTaskStatusCommand(Guid taskId, TaskStatus status)
        {
            TaskId = taskId;
            Status = status;
        }
    }
}
