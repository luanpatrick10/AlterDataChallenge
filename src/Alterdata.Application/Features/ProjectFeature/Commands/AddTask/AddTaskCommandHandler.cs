using System;
using System.Threading;
using System.Threading.Tasks;
using Alterdata.Domain.Repositories;
using Shared.Mediator;
using TaskEntity = Alterdata.Domain.Entities.Task;

namespace Alterdata.Application.Features.ProjectFeature.Commands.AddTask
{
    public class AddTaskCommandHandler : IRequestHandler<AddTaskCommand, Guid>
    {
        private readonly IProjectRepository _projectRepository;

        public AddTaskCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Guid> Handle(AddTaskCommand request, CancellationToken cancellationToken)
        {            
            var project =  await _projectRepository.GetByIdAsync(request.ProjectId);
            if (project == null)
                throw new Exception("Project not found");

            var task = new TaskEntity(request.Title, request.Description, request.DueDate, request.ProjectId);                        
            await _projectRepository.AddTaskAsync(task);
            await _projectRepository.SaveChangesAsync();
            return task.Id;
        }
    }
}
