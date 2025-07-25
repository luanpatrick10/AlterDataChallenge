namespace Shared.Exceptions;

public class DomainException : Exception
{
    public DomainException(string message = "Domain error occurred.") : base(message)
    {
    }
    public DomainException(string message, Exception innerException) : base(message, innerException)
    {
    }
}