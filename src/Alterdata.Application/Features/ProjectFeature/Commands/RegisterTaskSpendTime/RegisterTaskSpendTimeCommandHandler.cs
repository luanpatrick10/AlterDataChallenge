using MediatR;
using Alterdata.Domain.Entities;
using Alterdata.Domain.Repositories;
using Shared.Exceptions;
using Task = Alterdata.Domain.Entities.Task;
using TaskStatus = Alterdata.Domain.Enums.TaskStatus;

namespace Alterdata.Application.Features.ProjectFeature.Commands.RegisterTaskSpendTime
{
    public class RegisterTaskSpendTimeCommandHandler : IRequestHandler<RegisterTaskSpendTimeCommand, Guid>
    {
        private readonly IProjectRepository _projectRepository;

        public RegisterTaskSpendTimeCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Guid> Handle(RegisterTaskSpendTimeCommand request, CancellationToken cancellationToken)
        {
            var task = await _projectRepository.GetTaskDetailsByIdAsync(request.TaskId);
            ValidateTaskForSpentTime(task, request.TaskId);
            var taskSpentTime = new TaskSpentTime(request.StartedAt, request.FinishedAt, request.TaskId);
            await _projectRepository.AddTaskSpentTimeAsync(taskSpentTime);
            return taskSpentTime.Id;
        }

        private static void ValidateTaskForSpentTime(Task? task, Guid taskId)
        {
            if (task == null)
                throw new NotFoundException();
            if (task.Status == TaskStatus.Concluído)
                throw new DomainException("Cannot add spent time to a completed task.");
        }
    }
}
