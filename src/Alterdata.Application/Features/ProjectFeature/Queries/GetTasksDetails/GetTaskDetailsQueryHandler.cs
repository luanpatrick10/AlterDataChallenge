using Alterdata.Domain.Repositories;
using Shared.Mediator;

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
            if (task == null)
                return null;

            var comments = task.TasksComment?.Select(tc => new TaskCommentDto
            {
                Id = tc.Id,
                Comment = tc.Text,
                CreatedAt = tc.CreateAt
            }).ToList() ?? new List<TaskCommentDto>();

            var spentTimes = task.SpentTimes?.Select(st => new TaskSpentTimeDto
            {
                Id = st.Id,
                Hours = (st.FinishedAt - st.StartedAt).TotalHours,
                Date = st.StartedAt
            }).ToList() ?? new List<TaskSpentTimeDto>();

            return new TaskDetailsDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                ProjectId = task.ProjectId,
                Status = task.Status.ToString(),
                TasksComment = comments,
                SpentTimes = spentTimes
            };
        }
    }
}
