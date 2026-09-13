namespace cyoa_core.src
{
    interface IHaveEffects 
    {
        List<Effect> Effects { get; }
        public void ApplyEffects(StoryState ss);
    }
}
