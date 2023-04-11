using MyNeuralNetwork.Domain.Entities.Nets.Generators;
using MyNeuralNetwork.Domain.Entities.Nets.Generators.Supports;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Managers;
using MyNeuralNetwork.Domain.Entities.Nets.Networks.Circuits.Backward;
using MyNeuralNetwork.Domain.Entities.Nets.Networks.Circuits.Forward;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Activations;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts;
using MyNeuralNetwork.Domain.Entities.Nets.Trainers;
using MyNeuralNetwork.Domain.Interfaces.Networks;
using MyNeuralNetwork.Domain.Tests.Entities.Support;
using MyNeuralNetwork.Tests.Utils;
using MyNeuralNetwork.Tests.Utils.Activations;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MyNeuralNetwork.Domain.Tests.Entities.Nets.Networks
{
    public class NeuralNetworkTests
    {
        [Test]
        public void TestIfGetNextLayerCorrectly()
        {
            var network = NetworkGenerator.GiveMeOne(new int[] { 1, 1, 1 });

            var numberOfLayer = 0;
            foreach (var layer in network.GetNextLayer())
            {
                Assert.That(layer.Label, Is.EqualTo(numberOfLayer++));
            }
        }

        [Test]
        public void TestIfPredictFunctionWorks()
        {
            var ngen = new Domain.Entities.Nets.Neurons.NeuronGenerator();
            ngen.WeightConfiguration.SetMaxAndMin(1, 1);
            ngen.BiasConfiguration.SetMaxAndMin(0, 0);

            var nngen = new NNGenerator(ngen, new LayersLinker());

            var neuronNetwork = nngen.Generate<SynapseManager, ActivationTester>(new int[] { 1, 1, 1 });

            var result = neuronNetwork.Predict(new Input[] { new Input(1) });

            Assert.That(result[0], Is.EqualTo(1));
        }

        [Test]
        public void TestXor()
        {
            var ngen = new Domain.Entities.Nets.Neurons.NeuronGenerator();
            var nngen = new NNGenerator(ngen, new LayersLinker());

            var neuralNetwork = nngen.Generate<SynapseManager, Tanh>(new int[] { 2, 6, 1 });
            DataManager dataManager = DataManagerGenerator.ForXor();

            var trainer = new Trainer(dataManager, neuralNetwork, new FeedForward(), new Backpropagation(), new TestLogger(10000));

            trainer.Fit(10000);

            TestContext.Write("Testing 1 - 0 (must be 1): ");
            var result = neuralNetwork.Predict(new Input[] { new Input(1), new Input(0) });
            TestContext.WriteLine(result[0]);
            Assert.That(result[0], Is.AtLeast(0.9));

            TestContext.Write("Testing 0 - 1 (must be 1): ");
            var result2 = neuralNetwork.Predict(new Input[] { new Input(0), new Input(1) });
            TestContext.WriteLine(result2[0]);
            Assert.That(result2[0], Is.AtLeast(0.9));

            TestContext.Write("Testing 0 - 0 (must be 0): ");
            var result3 = neuralNetwork.Predict(new Input[] { new Input(0), new Input(0) });
            Assert.That(result3[0], Is.LessThan(0.1));
            TestContext.WriteLine(result3[0]);

            TestContext.Write("Testing 1 - 1 (must be 0): ");
            var result4 = neuralNetwork.Predict(new Input[] { new Input(1), new Input(1) });
            Assert.That(result4[0], Is.LessThan(0.1));
            TestContext.WriteLine(result4[0]);
        }


        [Test]
        public void TestIfItIsSortedCorrectly()
        {
            var ngen = new Domain.Entities.Nets.Neurons.NeuronGenerator();
            var nngen = new NNGenerator(ngen, new LayersLinker());
            var fitnessWinnerNet = nngen.Generate<SynapseManager, Tanh>(new int[] { 2, 6, 1 });
            fitnessWinnerNet.Fitness = 1000;
            var nets = new List<INeuralNetwork>() {
                fitnessWinnerNet,
                nngen.Generate<SynapseManager, Tanh>(new int[] { 2, 6, 1 }),
                nngen.Generate<SynapseManager, Tanh>(new int[] { 2, 6, 1 }),
                nngen.Generate<SynapseManager, Tanh>(new int[] { 2, 6, 1 }),
                nngen.Generate<SynapseManager, Tanh>(new int[] { 2, 6, 1 }),
                nngen.Generate<SynapseManager, Tanh>(new int[] { 2, 6, 1 }),
            };

            Assert.That(nets.Last().Fitness, Is.Not.EqualTo(1000));

            nets.Sort();

            Assert.That(nets.Last().Fitness, Is.EqualTo(1000));
        }
    }
}