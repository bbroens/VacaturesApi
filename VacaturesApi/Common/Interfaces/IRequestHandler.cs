namespace VacaturesApi.Common.Interfaces;

/// <summary>
/// Interface for request handlers. Implementations will be automatically registered to the DI container.
/// </summary>

public interface IRequestHandler<TRequest, TResult> 
    where TRequest : IRequest<TResult>
{
    Task<TResult> Handle(TRequest request, CancellationToken cancellationToken);
}