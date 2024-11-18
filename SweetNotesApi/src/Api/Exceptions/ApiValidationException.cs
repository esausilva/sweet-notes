namespace Api.Exceptions;

public class ApiValidationException(IDictionary<string, string[]> errors) : Exception
{
    public IDictionary<string,string[]> Errors { get; } = errors;
}