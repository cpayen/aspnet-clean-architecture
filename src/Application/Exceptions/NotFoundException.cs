namespace Application.Exceptions;

public class NotFoundException : ApplicationException
{
    public NotFoundException(string message) : base("Not found", message) {}
    
    public NotFoundException(string entityType, string identifier)
        : this($"{entityType} entity with identifier [{identifier}] not found") {}
}