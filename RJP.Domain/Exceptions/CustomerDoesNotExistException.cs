namespace RJP.Domain.Exceptions;

public class CustomerDoesNotExistException : Exception
{
    public CustomerDoesNotExistException(string message) : base(message)
    {

    }
}
