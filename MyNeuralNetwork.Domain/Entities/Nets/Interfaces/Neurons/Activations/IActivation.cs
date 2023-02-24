namespace MyNeuralNetwork.Domain.Entities.Nets.Interfaces.Neurons.Activations
{
    public interface IActivation
    {
        float Activate(float x);
        float Derivative(float x);
    }
}
