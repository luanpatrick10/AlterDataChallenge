using Shared.Events;

namespace Alterdata.Domain.Events
{
  public class ProjectCreatedEvent : Event
  {
    public Guid ProjectId { get; }
    public string ProjectName { get; }

    public ProjectCreatedEvent(Guid projectId, string projectName)
    {
      ProjectId = projectId;
      ProjectName = projectName;
    }
  }
}