using MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics;
using MyNeuralNetwork.Domain.Interfaces.Neurons.Activations;

namespace MyNeuralNetwork.Tests.Utils.Activations
{
    public class ActivationTester : IActivator
    {

        public double Activate(double x)
        {
            return x;
        }

        public NeuralDoubleValue Activate(NeuralDoubleValue x)
        {
            return x;
        }

        public double Derivative(double x)
        {
            return x;
        }

        public NeuralDoubleValue Derivative(NeuralDoubleValue x)
        {
            return x;
        }
    }

}
