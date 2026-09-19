using cyoa_core.src.exceptions;

namespace cyoa_core.src
{
    public class BooleanCondition : Condition
    {
        public bool Expected { get; set; }

        public override bool IsMet(StoryState ss)
        {
            var variable = ss.Variables[Target.Guid] ?? throw new ArgumentNullException();

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
