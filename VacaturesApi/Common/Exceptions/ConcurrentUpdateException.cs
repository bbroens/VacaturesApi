namespace VacaturesApi.Common.Exceptions;

public class ConcurrentUpdateException : Exception
{
    public ConcurrentUpdateException(string name, object key) : base($"Entity {name} ({key}) seems to have been updated in the meantime.") { }
}