using MyNeuralNetwork.Domain.Entities.Nets.Generators;
using MyNeuralNetwork.Domain.Entities.Nets.Networks;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts;
using MyNeuralNetwork.Tests.Utils.Activations;

namespace MyNeuralNetwork.Tests.Utils
{
    public class NetworkGeneratorForTests
    {
        public static NeuralNetwork GiveMeOne(int[] sizeOfLayers, bool random=true)
        {
            var neuronGenerator = new NeuronGenerator();
            if(!random)
            {
                neuronGenerator.WeightConfiguration.SetMaxAndMin(1, 1);
                neuronGenerator.BiasConfiguration.SetMaxAndMin(0, 0);
            }

            var nNGenerator = new NNGenerator(neuronGenerator);
            var network = nNGenerator.Generate<SynapseManager, ActivationTester>(sizeOfLayers);

            return network;
        }
    }
}
