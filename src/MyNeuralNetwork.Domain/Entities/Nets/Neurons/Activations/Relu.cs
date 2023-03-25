using MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics;
using MyNeuralNetwork.Domain.Interfaces.Neurons.Activations;

namespace MyNeuralNetwork.Domain.Entities.Nets.Neurons.Activations
{
    public class Relu : IActivator
    {
        public double Activate(double x)
        {
            return (0 >= x) ? 0 : x;
        }

        public NeuralDoubleValue Activate(NeuralDoubleValue x)
        {
            return new(Activate(x.Value));
        }

        public double Derivative(double x)
        {
            return (0 >= x) ? 0 : 1;
        }

        public NeuralDoubleValue Derivative(NeuralDoubleValue x)
        {
            return new(Derivative(x.Value));
        }
    }
}
