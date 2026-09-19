namespace cyoa_core.src
{
    public class Choice : IHaveEffects
    {
        public Guid Guid { get; } = Guid.NewGuid();
        public required string Label { get; set; }
        public Condition? Gate { get; set; }
        public required Node Next { get; set; }
        public List<Effect> Effects { get; set; } = [];

        public void ApplyEffects(StoryState ss)
        {
            foreach (var effect in Effects) {
                effect.Apply(ss);
            }
        }
    }
}