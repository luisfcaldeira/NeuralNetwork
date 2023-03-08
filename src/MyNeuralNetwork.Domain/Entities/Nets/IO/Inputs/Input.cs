using MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics;

namespace MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs
{
    public class Input : FloatNeuralValue
    {
        public Input() : base(0)
        {
        }

        public Input(float value) : base(value)
        {
        }
    }
}
