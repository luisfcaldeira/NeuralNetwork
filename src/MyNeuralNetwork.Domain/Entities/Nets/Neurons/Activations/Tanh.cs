using MyNeuralNetwork.Domain.Entities.Nets.Interfaces.Neurons.Activations;
using System;

namespace MyNeuralNetwork.Domain.Entities.Nets.Neurons.Activations
{
    public class Tanh : IActivation
    {
        public float Activate(float x)
        {
            return (float)Math.Tanh(x);
        }

        public float Derivative(float x)
        {
            return 1 - (x * x);
        }
    }
}
