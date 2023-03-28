using MyNeuralNetwork.Domain.Entities.Nets.Generators;
using MyNeuralNetwork.Domain.Entities.Nets.Generators.Supports;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts;
using MyNeuralNetwork.Tests.Utils;
using MyNeuralNetwork.Tests.Utils.Activations;
using NUnit.Framework;

namespace MyNeuralNetwork.Domain.Tests.Entities.Nets.Networks
{
    public class NetworkTests
    {
        [Test]
        public void TestIfGetNextLayerCorrectly()
        {
            var network = NetworkGeneratorForTests.GiveMeOne(new int[] { 1, 1, 1 });

            var numberOfLayer = 0;
            foreach (var layer in network.GetNextLayer()) {
                Assert.That(layer.Label, Is.EqualTo(numberOfLayer++));
            }
        }


        [Test]
        public void TestIfPredictFunctionWorks()
        {
            var ngen = new NeuronGenerator();
            ngen.WeightConfiguration.SetMaxAndMin(1, 1);
            ngen.BiasConfiguration.SetMaxAndMin(0, 0);

            var nngen = new NNGenerator(ngen, new LayersLinker());

            var neuronNetwork = nngen.Generate<SynapseManager, ActivationTester>(new int[] { 1, 1, 1 });

            var result = neuronNetwork.Predict(new Input[] { new Input(1) });

            Assert.That(result[0], Is.EqualTo(1));
        }

    }
}
