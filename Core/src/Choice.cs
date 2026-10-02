namespace Core.src
{
    public class Choice : IHaveEffects
    {
        public Guid Guid { get; } = Guid.NewGuid();
        public required string Label { get; set; }
        public Condition? Gate { get; set; }
        public required Node Next { get; set; }
        public List<Effect> Effects { get; set; } = [];

        public void ApplyEffects(Dictionary<Guid, Variable> variablesByGuid)
        {
            foreach (var effect in Effects) {
                effect.Apply(variablesByGuid);
            }
        }
    }
}