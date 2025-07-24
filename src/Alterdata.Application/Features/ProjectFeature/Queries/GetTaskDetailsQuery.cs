using MediatR;
using System;
using System.Collections.Generic;

namespace Alterdata.Application.Features.ProjectFeature.Queries
{
    public class GetTaskDetailsQuery : Shared.Mediator.IRequest<TaskDetailsDto>
    {
        public Guid TaskId { get; set; }
        public GetTaskDetailsQuery(Guid taskId)
        {
            TaskId = taskId;
        }
    }

    public class TaskDetailsDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Guid ProjectId { get; set; }
        public string Status { get; set; }
        public ICollection<TaskCommentDto> TasksComment { get; set; }
        public ICollection<TaskSpentTimeDto> SpentTimes { get; set; }
    }

    public class TaskCommentDto
    {
        public Guid Id { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class TaskSpentTimeDto
    {
        public Guid Id { get; set; }
        public double Hours { get; set; }
        public DateTime Date { get; set; }
    }
}
