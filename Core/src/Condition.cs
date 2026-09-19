namespace cyoa_core.src
{
    public abstract class Condition
    {
        public required Variable Target { get; set; }
        public abstract bool IsMet(StoryState ss);
    }
}