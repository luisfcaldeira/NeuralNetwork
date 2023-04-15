using MyNeuralNetwork.Domain.Entities.Nets.Generators;
using MyNeuralNetwork.Domain.Entities.Nets.Generators.Supports;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.Networks.Circuits.Forward;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts;
using MyNeuralNetwork.Tests.Utils.Activations;
using NUnit.Framework;
using System.Linq;

namespace MyNeuralNetwork.Domain.Tests.Entities.Nets.Circuits.Foward
{
    public class FeedFowardTests
    {
        [Test]
        public void TestIfFeedFowardPassValueThroughTheNetwork()
        {
            NeuronGenerator neuronGenerator = new NeuronGenerator();
            neuronGenerator.WeightConfiguration.SetMaxAndMin(1, 1);
            neuronGenerator.BiasConfiguration.SetMaxAndMin(0, 0);

            NNGenerator nngen = new NNGenerator(neuronGenerator, new LayersLinker());
            FeedForward feedFoward = new FeedForward();
            var neuralNetwork = nngen.Generate<SynapseManager, ActivationTester>(new int[] { 1, 1, 1 });
            Assert.That(neuralNetwork.Layers.Last().Output.Count, Is.EqualTo(0));
            feedFoward.Send(neuralNetwork, new Input[] { new Input(1) });
            Assert.That(neuralNetwork.Layers.Last().Output.Count, Is.EqualTo(1));
            Assert.That(neuralNetwork.Layers.Last().Output[0], Is.EqualTo(1));
        }
    }
}
