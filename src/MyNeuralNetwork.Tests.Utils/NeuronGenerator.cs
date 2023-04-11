using MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts;
using MyNeuralNetwork.Tests.Utils.Activations;

namespace MyNeuralNetwork.Tests.Utils
{

    public static class NeuronGenerator
    {

        public static Neuron MakeEmptyNeuron()
        {
            return new Neuron(new
                ActivationTester(), 
                new RandomDoubleValue(),
                new SynapseManager());
        }
    }
}
