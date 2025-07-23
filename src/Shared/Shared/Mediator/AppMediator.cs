using MediatR;

namespace Shared.Mediator;

public class AppMediator
{
    private readonly IMediator _mediator;

    public AppMediator(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<TResult> Send<TResult>(IRequest<TResult> request, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send((object)request, cancellationToken) is TResult result ? result : default!;
    }

    public async Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
    {
        await _mediator.Publish((object)notification, cancellationToken);
    }
}