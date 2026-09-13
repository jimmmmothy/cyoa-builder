using cyoa_core.src.exceptions;

namespace cyoa_core.src
{
    class BooleanEffect : Effect
    {
        public BooleanOp Op { get; set; }
        public bool Value { get; set; }

        public override void Apply(StoryState ss)
        {
            var variable = ss.Variables[Target.Name] ?? throw new ArgumentNullException();

            if (variable is BooleanVariable boolVar)
            {
                boolVar.Value = Op switch
                {
                    BooleanOp.SET => Value,
                    BooleanOp.TOGGLE => !boolVar.Value,
                    _ => Value,
                };
            }
            else
            {
                throw new VariableTypeMismatchException();
            }
        }
    }
}
