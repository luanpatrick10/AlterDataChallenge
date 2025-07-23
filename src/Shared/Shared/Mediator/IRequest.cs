namespace Shared.Mediator;

public interface IRequest : IRequest<Unit> { }

public interface IRequest<TResult>
{
}