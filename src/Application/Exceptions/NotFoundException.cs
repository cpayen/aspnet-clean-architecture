namespace Application.Exceptions;

public class NotFoundException : ApplicationException
{
    public NotFoundException() {}
    public NotFoundException(string message) : base(message) {}
    
    public NotFoundException(string entityType, string identifier)
        : this($"{entityType} entity with identifier [{identifier}] not found") {}
}