using MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics;

namespace MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts
{
    public class Transmition : NeuralDoubleValue
    {
        public Transmition()
        {
        }

        public Transmition(NeuralDoubleValue v) : base(v.Value)
        {
        }

        public Transmition(double value) : base(value)
        {
        }
    }
}
