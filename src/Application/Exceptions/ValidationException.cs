namespace Application.Exceptions;

public sealed class ValidationException : ApplicationException
{
    public ValidationException(IReadOnlyDictionary<string, string[]> errorsDictionary)
        : base("Validation error")
        => ErrorsDictionary = errorsDictionary;

    public IReadOnlyDictionary<string, string[]> ErrorsDictionary { get; }
}