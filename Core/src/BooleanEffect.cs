using cyoa_core.src.exceptions;

namespace cyoa_core.src
{
    public class BooleanEffect : Effect
    {
        public BooleanOp Op { get; set; }
        public bool Value { get; set; }

        // Think about whether, in the case of non-existing variable, it would be better to
        // add it to the SS dict, or just clone SD variable declarations into SS from the beginning?
        public override void Apply(StoryState ss)
        {
            var variable = ss.Variables[Target.Guid] ?? throw new ArgumentNullException();

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
