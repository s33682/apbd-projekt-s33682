namespace WebApp.Exceptions;

public class AlreadyDoneException : Exception
{
    public AlreadyDoneException()
    {
    }

    public AlreadyDoneException(string? message) : base(message)
    {
    }

    public AlreadyDoneException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}