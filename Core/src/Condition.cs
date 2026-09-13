namespace cyoa_core.src
{
    abstract class Condition
    {
        public required Variable Target { get; set; }
        public abstract bool IsMet(StoryState ss);
    }
}