namespace VacaturesApi.Common.Exceptions;

/// <summary>
/// Exception to be thrown when an entity was updated in the meantime.
/// </summary>

public class ConcurrentUpdateException : Exception
{
    public ConcurrentUpdateException(string name, object key) : base($"Entity {name} ({key}) seems to have been updated in the meantime.") { }
}