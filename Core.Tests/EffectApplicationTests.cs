using cyoa_core.src;

namespace cyoa_core_tests;

public class EffectApplicationTests
{
    private readonly StoryState ss;

    public EffectApplicationTests()
    {
        ss = TestUtilities.CreateStoryState();
    }

    [Theory]
    [InlineData(BooleanOp.SET)]
    [InlineData(BooleanOp.TOGGLE)]
    public void BooleanEffects_ShouldApply_WhenTargetIsValid(BooleanOp op)
    {
        BooleanVariable testTarget1 = TestUtilities.GetVar<BooleanVariable>(ss, "Boolean 0");
        BooleanEffect effect1 = new() { Op = op, Target = testTarget1, Value = false };

        BooleanVariable testTarget2 = TestUtilities.GetVar<BooleanVariable>(ss, "Boolean 2");
        BooleanEffect effect2 = new() { Op = op, Target = testTarget2, Value = false };

        effect1.Apply(ss);
        effect2.Apply(ss);

        Assert.False((ss.Variables[testTarget1.Guid] as BooleanVariable)!.Value);
        Assert.False((ss.Variables[testTarget2.Guid] as BooleanVariable)!.Value);

        if (op == BooleanOp.TOGGLE)
        {
            effect1.Apply(ss);
            effect2.Apply(ss);

            Assert.True((ss.Variables[testTarget1.Guid] as BooleanVariable)!.Value);
            Assert.True((ss.Variables[testTarget2.Guid] as BooleanVariable)!.Value);
        }
    }

    [Theory]
    [InlineData(BooleanOp.SET)]
    [InlineData(BooleanOp.TOGGLE)]
    public void BooleanEffects_ShouldFail_WhenTargetIsInvalid(BooleanOp op)
    {
        BooleanVariable testTarget1 = new() { Name = "Invalid1", Value = false};
        BooleanEffect effect1 = new() { Op = op, Target = testTarget1, Value = false };

        BooleanVariable testTarget2 = new() { Name = "Invalid2", Value = false};
        BooleanEffect effect2 = new() { Op = op, Target = testTarget2, Value = false };

        Assert.Throws<KeyNotFoundException>(() => effect1.Apply(ss));
        Assert.Throws<KeyNotFoundException>(() => effect1.Apply(ss));
    }

    [Theory]
    [InlineData(NumericOp.SET)]
    [InlineData(NumericOp.ADD)]
    [InlineData(NumericOp.MULTIPLY)]
    public void NumericEffects_ShouldApply_WhenTargetIsValid(NumericOp op)
    {
        NumericVariable testTarget1 = TestUtilities.GetVar<NumericVariable>(ss, "Numeric 1");
        NumericEffect effect1 = new() { Op = op, Target = testTarget1, Value = 999 };

        NumericVariable testTarget2 = TestUtilities.GetVar<NumericVariable>(ss, "Numeric 3");
        NumericEffect effect2 = new() { Op = op, Target = testTarget2, Value = -1.0 / 2 };

        for (int i = 0; i < 2; i++)
        {
            double expected1;
            double expected2;

            switch (op)
            {
                case NumericOp.ADD:
                    expected1 = testTarget1.Value + effect1.Value;
                    expected2 = testTarget2.Value + effect2.Value;
                    break;
                case NumericOp.MULTIPLY:
                    expected1 = testTarget1.Value * effect1.Value;
                    expected2 = testTarget2.Value * effect2.Value;
                    break;
                default:
                    expected1 = effect1.Value;
                    expected2 = effect2.Value;
                    break;
            }

            effect1.Apply(ss);
            effect2.Apply(ss);

            Assert.Equal(expected1, testTarget1.Value);
            Assert.Equal(expected2, testTarget2.Value);
        }
    }

    [Theory]
    [InlineData(NumericOp.SET)]
    [InlineData(NumericOp.ADD)]
    [InlineData(NumericOp.MULTIPLY)]
    public void NumericEffects_ShouldFail_WhenTargetIsInvalid(NumericOp op)
    {
        NumericVariable testTarget1 = new() { Name = "Invalid1", Value = 999};
        NumericEffect effect1 = new() { Op = op, Target = testTarget1, Value = 999 };

        NumericVariable testTarget2 = new() { Name = "Invalid2", Value = -1.0 / 2};
        NumericEffect effect2 = new() { Op = op, Target = testTarget2, Value = -1.0 / 2 };

        Assert.Throws<KeyNotFoundException>(() => effect1.Apply(ss));
        Assert.Throws<KeyNotFoundException>(() => effect1.Apply(ss));
    }
}