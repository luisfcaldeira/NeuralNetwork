using MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics;

namespace MyNeuralNetwork.Domain.Entities.Nets.Interfaces.Neurons.Activations
{
    public interface IActivator
    {
        double Activate(double x);
        double Derivative(double x);
        NeuralDoubleValue Activate(NeuralDoubleValue x);
        NeuralDoubleValue Derivative(NeuralDoubleValue x);
    }
}
