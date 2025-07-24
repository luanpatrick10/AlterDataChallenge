using Alterdata.Domain.Entities;
using Alterdata.Domain.Repositories;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Alterdata.Application.Features.ProjectFeature.Queries
{
    public class GetTaskDetailsQueryHandler : IRequestHandler<GetTaskDetailsQuery, TaskDetailsDto>
    {
        private readonly IProjectRepository _projectRepository;
        public GetTaskDetailsQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<TaskDetailsDto> Handle(GetTaskDetailsQuery request, CancellationToken cancellationToken)
        {
            var task = await _projectRepository.GetTaskDetailsByIdAsync(request.TaskId);
            if (task == null) return null;

            return new TaskDetailsDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                ProjectId = task.ProjectId,
                Status = task.Status.ToString(),
                TasksComment = task.TasksComment?.Select(tc => new TaskCommentDto
                {
                    Id = tc.Id,
                    Comment = tc.Text,
                    CreatedAt = tc.CreateAt
                }).ToList(),
                SpentTimes = task.SpentTimes?.Select(st => new TaskSpentTimeDto
                {
                    Id = st.Id,
                    Hours = (st.FinishedAt - st.StartedAt).TotalHours,
                    Date = st.StartedAt
                }).ToList()
            };
        }
    }
}
