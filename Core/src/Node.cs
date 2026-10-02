namespace Core.src
{
    public abstract class Node
    {
        public Guid Guid { get; } = Guid.NewGuid();
    }
}
