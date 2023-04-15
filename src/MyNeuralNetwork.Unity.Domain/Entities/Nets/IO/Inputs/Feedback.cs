using MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Outputs;

namespace MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs
{
    public class Feedback : NeuralDoubleValue
    {
        public Feedback(double value, Expected expected) : base(value - expected.Value)
        {
        }

        public Feedback(Output output, Expected expected) : base(output.Value - expected.Value)
        {
        }
    }
}
