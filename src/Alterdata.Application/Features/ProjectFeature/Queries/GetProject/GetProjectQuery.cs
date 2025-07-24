using System;
using System.Collections.Generic;
using Shared.Mediator;

namespace Alterdata.Application.Features.ProjectFeature.Queries.GetProject
{
    public class GetProjectQuery : IRequest<GetProjectDto>
    {
        public Guid ProjectId { get; set; }
        public GetProjectQuery(Guid projectId)
        {
            ProjectId = projectId;
        }
    }

    public class GetProjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<GetProjectTaskDto> Tasks { get; set; }
    }

    public class GetProjectTaskDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
    }
}
