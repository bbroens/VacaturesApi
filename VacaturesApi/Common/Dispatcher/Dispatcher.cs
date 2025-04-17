using VacaturesApi.Common.Interfaces;

namespace VacaturesApi.Common.Dispatcher;

/// <summary>
/// Finds and resolves the appropriate handler for a request and wraps it with implemented behaviors.
/// </summary>

public class Dispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public Dispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResult> DispatchAsync<TRequest, TResult>(TRequest request, CancellationToken cancellationToken)
        where TRequest : IRequest<TResult>
    {
        // Resolve behaviors from the DI container for each request
        var behaviors = _serviceProvider.GetServices<IRequestBehavior<TRequest, TResult>>();

        // Resolve the handler from the DI container for each request
        var handler = _serviceProvider.GetService<IRequestHandler<TRequest, TResult>>();

        if (handler == null)
        {
            throw new InvalidOperationException($"No handler found for request {typeof(TRequest).Name}");
        }

        // Create delegate for Handle method
        RequestHandlerDelegate<TResult> handlerDelegate = () => handler.Handle(request, cancellationToken);

        // Wrap request behaviors to be executed before handler
        foreach (var behavior in behaviors.Reverse())
        {
            var next = handlerDelegate;
            handlerDelegate = () => behavior.Process(request, next, cancellationToken);
        }

        return await handlerDelegate();
    }
}