namespace cyoa_core.src
{
    class StoryState
    {
        public Dictionary<string, Variable> Variables { get; } = [];
        public Stack<Node> History { get; } = new Stack<Node>();
        public Dictionary<Chapter, ChapterCheckpoint> Checkpoints { get; } = [];
    }
}
