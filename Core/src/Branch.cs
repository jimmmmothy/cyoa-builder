namespace cyoa_core.src
{
    class Branch: Node
    {
        public List<Choice> Choices { get; set; } = [];
    }
}