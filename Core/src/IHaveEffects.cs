namespace Core.src
{
    public interface IHaveEffects 
    {
        List<Effect> Effects { get; }
        public void ApplyEffects(Dictionary<Guid, Variable> variablesByGuid);
    }
}
