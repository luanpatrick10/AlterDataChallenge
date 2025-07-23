using MediatR;

namespace Shared.Mediator;

public interface IRequestHandler<TRequest> : MediatR.IRequestHandler<TRequest, Unit> where TRequest : IRequest<Unit> { }

public interface IRequestHandler<TRequest, TResult> : MediatR.IRequestHandler<TRequest, TResult> where TRequest : IRequest<TResult>
{    
}