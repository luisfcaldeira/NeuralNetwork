using MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics;

namespace MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts
{
    internal class Transmition : NeuralFloatValue
    {
        public Transmition()
        {
        }

        public Transmition(NeuralFloatValue floatNeuralValue) : base(floatNeuralValue)
        {
        }

        public Transmition(float value) : base(value)
        {
        }
    }
}
