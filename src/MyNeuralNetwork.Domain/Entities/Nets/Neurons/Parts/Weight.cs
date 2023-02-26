using MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics;
using MyNeuralNetwork.Domain.Entities.Support;

namespace MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts
{
    public class Weight : FloatField
    {

        public Weight()
        {
            Value = MyRandom.Range(-0.5f, 0.5f);
            Value = 1;
        }

        internal void Fix(float value)
        {
            Value -= value;
        }
    }
}
