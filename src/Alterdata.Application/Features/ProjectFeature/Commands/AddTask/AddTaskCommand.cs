using System;
using Shared.Mediator;

namespace Alterdata.Application.Features.ProjectFeature.Commands.AddTask
{
    public class AddTaskCommand : IRequest<Guid>
    {
        public Guid ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
    }
}
