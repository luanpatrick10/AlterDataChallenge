using Shared.Mediator;

namespace Alterdata.Application.Features.ProjectFeature.Commands.RegisterTaskSpendTime
{
    public class RegisterTaskSpendTimeCommand : IRequest<Guid>
    {
        public Guid TaskId { get; private set; }
        public DateTime StartedAt { get; set; }
        public DateTime FinishedAt { get; set; }

        public void SetTaskId(Guid taskId)
        {
            TaskId = taskId;
        }
    }
}
