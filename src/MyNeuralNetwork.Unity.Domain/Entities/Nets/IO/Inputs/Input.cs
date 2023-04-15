using MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics;

namespace MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs
{
    public class Input : NeuralDoubleValue
    {
        public Input() : base(0)
        {
        }

        public Input(double value) : base(value)
        {
        }
    }
}
