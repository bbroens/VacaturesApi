namespace VacaturesApi.Common.Interfaces;

/// <summary>
/// Interface for request behaviors to be executed before the request handler.
/// </summary>

public interface IRequestBehavior<in TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    Task<TResponse> Process(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken);
}

public delegate Task<TResponse> RequestHandlerDelegate<TResponse>();