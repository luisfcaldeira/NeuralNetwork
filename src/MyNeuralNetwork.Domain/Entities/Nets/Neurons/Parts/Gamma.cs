using MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics;
using MyNeuralNetwork.Domain.Entities.Nets.Interfaces.Neurons.Activations;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;

namespace MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts
{
    public class Gamma : FloatField
    {
        internal IActivation Activation { get; }

        public Gamma(IActivation activation)
        {
            Activation = activation;
        }

        public void SetValue(Feedback feedback, Neuron neuron)
        {
            Value = feedback.Value * Activation.Derivative(neuron.Value);
        }

    }
}
