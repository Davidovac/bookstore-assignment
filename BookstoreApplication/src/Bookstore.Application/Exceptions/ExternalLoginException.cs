namespace Bookstore.Application.Exceptions
{
    public class ExternalLoginException : Exception
    {
        public ExternalLoginException(string message) : base(message)
        {
        }
    }
}
