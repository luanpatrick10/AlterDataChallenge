using MediatR;

namespace Shared.Mediator;

public interface IRequest : MediatR.IRequest<Unit> { }

public interface IRequest<TResult> : MediatR.IRequest<TResult>
{
}