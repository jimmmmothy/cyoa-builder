namespace cyoa_core.src
{
    public class ConditionalRoute(Condition when, Node then)
    {
        public Condition When { get; } = when;
        public Node Then { get; } = then;
    }
}