using MyNeuralNetwork.Domain.Entities.Support;

namespace MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics
{
    public class RandomFloatValue : NeuralFloatValue
    {
        public const float DefaultMinimumRange = -0.5f;
        public const float DefaultMaximumRange = 0.5f;

        public RandomFloatValue() : base(MyRandom.Range(DefaultMinimumRange, DefaultMaximumRange))
        {
        }

        public RandomFloatValue(float value) : base(value)
        {
        }

        public RandomFloatValue(float min, float max) : base(MyRandom.Range(min, max))
        {
        }
    }
}
