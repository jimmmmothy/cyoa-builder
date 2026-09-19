namespace cyoa_core.src
{
    public class ConditionalBranch(Node defNode) : Node
    {
        public List<ConditionalRoute> Routes { get; set; } = [];
        public Node Default { get; set; } = defNode;
    }
}