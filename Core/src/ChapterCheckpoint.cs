namespace cyoa_core.src
{
    class ChapterCheckpoint(Node entry, List<Variable> variables)
    {
        public Node EntryNode { get; } = entry;
        public List<Variable> VariablesSnapshot { get; } = [.. variables.Select(v => v.Clone())];
    }
}