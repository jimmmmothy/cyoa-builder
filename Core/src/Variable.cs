namespace cyoa_core.src
{
    abstract class Variable()
    {
        public required string Name { get; set; }
        public abstract Variable Clone();
    }
}
