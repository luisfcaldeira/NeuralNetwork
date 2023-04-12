using MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics;
using MyNeuralNetwork.Domain.Interfaces.Neurons.Activations;
using System;

namespace MyNeuralNetwork.Domain.Entities.Nets.Neurons.Activations
{
    public class Tanh : IActivator
    {
        public double Activate(double x)
        {
            return (double)Math.Tanh(x);
        }

        public NeuralDoubleValue Activate(NeuralDoubleValue x)
        {
            return new NeuralDoubleValue(Activate(x.Value));
        }

        public double Derivative(double x)
        {
            return 1 - (x * x);
        }

        public NeuralDoubleValue Derivative(NeuralDoubleValue x)
        {
            return new NeuralDoubleValue(Derivative(x.Value));
        }
    }
}
