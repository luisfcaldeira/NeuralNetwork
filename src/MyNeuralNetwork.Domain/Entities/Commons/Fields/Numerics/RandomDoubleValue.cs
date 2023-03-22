using MyNeuralNetwork.Domain.Entities.Support;

namespace MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics
{
    public class RandomDoubleValue : NeuralDoubleValue
    {
        public const double DefaultMinimumRange = -0.5;
        public const double DefaultMaximumRange = 0.5;

        public RandomDoubleValue() : base(MyRandom.Range(DefaultMinimumRange, DefaultMaximumRange))
        {
        }

        public RandomDoubleValue(double value) : base(value)
        {
        }

        public RandomDoubleValue(double min, double max) : base(MyRandom.Range(min, max))
        {
        }
    }
}
