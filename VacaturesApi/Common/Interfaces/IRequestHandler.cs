namespace VacaturesApi.Common.Interfaces;

/// <summary>
/// Interface for request handlers to be executed on specified request types.
/// Implementations will be automatically registered to the DI container
/// and then matched to a request type by the Dispatcher.
/// </summary>

public interface IRequestHandler<in TRequest, TResult> 
    where TRequest : IRequest<TResult>
{
    Task<TResult> Handle(TRequest request, CancellationToken cancellationToken);
}