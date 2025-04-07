namespace BookStoreTask.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(string message) : base(message)
        {
        }

        public BadRequestException(string message, string details) : base(message, details)
        {
        }
    }
}