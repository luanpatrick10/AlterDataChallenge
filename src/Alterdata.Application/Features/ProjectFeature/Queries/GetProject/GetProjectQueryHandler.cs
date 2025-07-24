using System.Threading;
using System.Threading.Tasks;
using Alterdata.Domain.Repositories;
using Shared.Mediator;
using Microsoft.EntityFrameworkCore;

namespace Alterdata.Application.Features.ProjectFeature.Queries.GetProject
{
    public class GetProjectQueryHandler : IRequestHandler<GetProjectQuery, GetProjectDto>
    {
        private readonly IProjectRepository _projectRepository;

        public GetProjectQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<GetProjectDto> Handle(GetProjectQuery request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository
                .GetProjectWithTasksOrDefaultAsync(request.ProjectId);

            if (project == null)
                return null;

            return new GetProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                Tasks = project.Tasks?.Select(t => new GetProjectTaskDto
                {
                    Id = t.Id,
                    Name = t.Title,
                    Description = t.Description,
                    Status = (int)t.Status
                }).ToList() ?? new List<GetProjectTaskDto>()
            };
        }
    }
}
