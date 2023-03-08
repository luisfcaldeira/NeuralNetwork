namespace MyNeuralNetwork.Domain.Entities.Nets.Interfaces.Neurons.Activations
{
    public interface IActivator
    {
        float Activate(float x);
        float Derivative(float x);
    }
}
