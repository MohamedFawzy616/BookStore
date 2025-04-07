namespace BookStoreTask.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, string details) : base(message, details)
        {
        }
    }
}