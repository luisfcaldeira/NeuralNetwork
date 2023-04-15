using MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics;
using MyNeuralNetwork.Domain.Interfaces.Neurons.Activations;
using System;

namespace MyNeuralNetwork.Domain.Entities.Nets.Neurons.Activations
{
    public class Sigmoid : IActivator
    {
        public double Activate(double x)
        {
            double k = (double)Math.Exp(x);
            return k / (1.0f + k);
        }

        public NeuralDoubleValue Activate(NeuralDoubleValue x)
        {
            return new NeuralDoubleValue(Activate(x.Value));
        }

        public double Derivative(double x)
        {
            return x * (1 - x);
        }

        public NeuralDoubleValue Derivative(NeuralDoubleValue x)
        {
            return new NeuralDoubleValue(Derivative(x.Value));
        }
    }
}
