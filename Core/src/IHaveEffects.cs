namespace cyoa_core.src
{
    public interface IHaveEffects 
    {
        List<Effect> Effects { get; }
        public void ApplyEffects(StoryState ss);
    }
}
