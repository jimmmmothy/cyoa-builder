using cyoa_core.src.exceptions;

namespace cyoa_core.src
{
    class BooleanCondition : Condition
    {
        public bool Expected { get; set; }

        public override bool IsMet(StoryState ss)
        {
            var variable = ss.Variables[Target.Name] ?? throw new ArgumentNullException();

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
