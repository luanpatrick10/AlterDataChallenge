using MediatR;
using System;

namespace Alterdata.Application.Features.ProjectFeature.Commands.CreateTaskComment
{
using Shared.Mediator;

    public class CreateTaskCommentCommand : IRequest<Guid>
    {
        public Guid TaskId { get; private set; }
        public required string Text { get; set; }

        public void SetTaskId(Guid taskId)
        {
            TaskId = taskId;
        }
        
    }
}
