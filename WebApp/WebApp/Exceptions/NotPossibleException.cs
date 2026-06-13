namespace WebApp.Exceptions;

public class NotPossibleException : Exception
{
    public NotPossibleException()
    {
    }

    public NotPossibleException(string? message) : base(message)
    {
    }

    public NotPossibleException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}