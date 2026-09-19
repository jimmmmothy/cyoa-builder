namespace cyoa_core.src
{
    public class Chapter
    {
        public Guid Guid { get; } = Guid.NewGuid();
        public required string Title { get; set; }
        public uint Order { get; set; }
        public List<Node> Nodes { get; set; } = [];
        public List<Node> EntryNodes { get; set; } = [];
    }
}
