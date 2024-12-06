namespace VacaturesApi.Common.Exceptions;

/// <summary>
/// Exception thrown when an entity is not found.
/// </summary>

public class NotFoundException : Exception
{
    public NotFoundException(string name, object key) : base($"Entity {name} ({key}) was not found.") { }
}