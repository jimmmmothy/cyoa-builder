namespace cyoa_core.src
{
    public abstract class Node
    {
        public Guid Guid { get; } = Guid.NewGuid();
    }
}
