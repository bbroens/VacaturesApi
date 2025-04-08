namespace VacaturesApi.Common.Interfaces;

/// <summary>
/// Marker interface to indicate that a request needs to be handled by the dispatcher.
/// </summary>

public interface IRequest<out TResult>
{
    
}