using Shared.Mediator;

namespace Shared.Events;

public abstract class Event : IEvent, INotification
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public DateTime OccurredOn { get; private set; } = DateTime.UtcNow;
}