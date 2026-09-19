namespace cyoa_core.src.exceptions
{
    public class NoNextException : Exception
    {
        public NoNextException() : base() {}
        public NoNextException(string message) : base(message) {}
    }
}