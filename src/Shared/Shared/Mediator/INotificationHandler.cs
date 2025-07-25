using INotificationHandlerMediator = MediatR.INotificationHandler<Shared.Mediator.INotification>;

namespace Shared.Mediator;

public interface INotificationHandler : INotificationHandlerMediator 
{    
}