namespace RJP.Domain.Exceptions
{
    public class AccountDoesNotExistException : Exception
    {
        public AccountDoesNotExistException(string message) : base(message)
        {

        }
    }
}
