namespace BookStoreTask.Exceptions
{
    public abstract class BaseException : Exception
    {
        public string? Details { get; }

        protected BaseException(string message) : base(message)
        {
        }

        protected BaseException(string message, string details) : base(message)
        {
            Details = details;
        }
    }
}