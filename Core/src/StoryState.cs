namespace cyoa_core.src
{
    public class StoryState
    {
        // Think about when applying effects, whether, in the case of non-existing variable,
        // it would be better to add it to the SS dict, or just clone SD variable declarations
        // into SS from the beginning?
        public Dictionary<Guid, Variable> Variables { get; } = [];
        public Stack<Node> History { get; } = new Stack<Node>();
        public Dictionary<Chapter, ChapterCheckpoint> Checkpoints { get; } = [];
    }
}
