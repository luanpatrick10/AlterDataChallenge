using Alterdata.Domain.Repositories;
using MediatR;

namespace Alterdata.Application.Features.ProjectFeature.Commands
{
    public class ChangeTaskStatusCommandHandler : IRequestHandler<ChangeTaskStatusCommand, bool>
    {
        private readonly IProjectRepository _projectRepository;
        public ChangeTaskStatusCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<bool> Handle(ChangeTaskStatusCommand request, CancellationToken cancellationToken)
        {
            var task = await _projectRepository.GetTaskDetailsByIdAsync(request.TaskId);
            if (task == null) return false;
                        
            task.SetStatus(request.Status);
            await _projectRepository.SaveChangesAsync();
            return true;
        }
    }
}
