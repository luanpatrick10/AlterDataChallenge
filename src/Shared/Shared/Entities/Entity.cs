using Shared.Events;

namespace Shared.Entities;

public abstract class Entity(dynamic id)
{
    public Guid Id { get; private set; } = id;
    public IEnumerable<IEvent>? Events { get; set; }
    
    public void AddEvent(IEvent @event)
    {
        Events ??= new List<IEvent>();
        ((List<IEvent>)Events).Add(@event);
    }
    public void ClearEvents()
    {
        Events = null;
    }
    public abstract void Validate();
}