using MediatR;
using Alterdata.Domain.Entities;
using Alterdata.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Alterdata.Application.Features.ProjectFeature.Commands.CreateTaskComment
{
    public class CreateTaskCommentCommandHandler : IRequestHandler<CreateTaskCommentCommand, Guid>
    {
        private readonly IProjectRepository _projectRepository;

        public CreateTaskCommentCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Guid> Handle(CreateTaskCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = new TaskComment(request.Text,request.TaskId);            
            await _projectRepository.AddTaskCommentAsync(comment);
            return comment.Id;
        }
    }
}
