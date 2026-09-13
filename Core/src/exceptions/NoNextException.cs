namespace cyoa_core.src.exceptions
{
    class NoNextException : Exception
    {
        public NoNextException() : base() {}
        public NoNextException(string message) : base(message) {}
    }
}