using Alterdata.Domain.Events;
using MediatR;

namespace Alterdata.Application.Subscribers.ProjectCreated
{
    public class EntityHistorySubscriber : INotificationHandler<ProjectCreatedEvent>
    {
        public async Task Handle(ProjectCreatedEvent notification, CancellationToken cancellationToken)
        {
            // Logic to handle the ProjectCreatedEvent
            // For example, you might log the event or update some entity history
            Console.WriteLine($"Project Created: {notification.ProjectId}, Name: {notification.ProjectName}");
            
            // Simulate some asynchronous operation
            await Task.CompletedTask;
        }

      
    }
}