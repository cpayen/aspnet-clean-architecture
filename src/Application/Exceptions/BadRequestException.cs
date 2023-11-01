namespace Application.Exceptions;

public class BadRequestException : ApplicationException
{
    public BadRequestException(string message) : base("Bad request", message) {}
}