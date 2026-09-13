namespace cyoa_core.src
{
    abstract class Effect 
    {
        public required Variable Target { get; set; }
        public abstract void Apply(StoryState ss);
    }
}
