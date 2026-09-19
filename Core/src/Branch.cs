namespace cyoa_core.src
{
    public class Branch: Node
    {
        public List<Choice> Choices { get; set; } = [];
    }
}