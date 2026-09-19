namespace cyoa_core.src
{
    public abstract class Variable()
    {
        public Guid Guid { get; } = Guid.NewGuid();
        public required string Name { get; set; }
        public abstract Variable Clone();
    }
}
