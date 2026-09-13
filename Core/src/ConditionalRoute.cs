namespace cyoa_core.src
{
    class ConditionalRoute(Condition when, Node then)
    {
        public Condition When { get; } = when;
        public Node Then { get; } = then;
    }
}