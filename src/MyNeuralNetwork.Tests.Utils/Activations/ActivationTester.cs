using MyNeuralNetwork.Domain.Entities.Nets.Interfaces.Neurons.Activations;

namespace MyNeuralNetwork.Tests.Utils.Activations
{
    public class ActivationTester : IActivator
    {
        
        public float Activate(float x)
        {
            return x;
        }

        public float Derivative(float x)
        {
            return x;
        }
    }

}
