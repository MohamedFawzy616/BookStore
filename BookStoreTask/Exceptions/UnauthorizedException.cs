namespace BookStoreTask.Exceptions
{
    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException(string message) : base(message)
        {
        }

        public UnauthorizedException(string message, string details) : base(message, details)
        {
        }
    }
}