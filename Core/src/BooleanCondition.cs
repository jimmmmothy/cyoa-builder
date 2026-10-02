using Core.src.exceptions;

namespace Core.src
{
    public class BooleanCondition : Condition
    {
        public bool Expected { get; set; }

        public override bool IsMet(Dictionary<Guid, Variable> variablesByGuid)
        {
            var variable = variablesByGuid[Target.Guid] ?? throw new ArgumentNullException();

            if (variable is BooleanVariable boolVar)
            {
                return boolVar.Value == Expected;
            }
            else
            {
                throw new VariableTypeMismatchException();
            }
        }
    }
}
