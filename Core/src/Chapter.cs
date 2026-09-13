namespace cyoa_core.src
{
    class Chapter
    {
        public required string Title { get; set; }
        public uint Order { get; set; }
        public List<Node> Nodes { get; set; } = [];
        public List<Node> EntryNodes { get; set; } = [];
    }
}
