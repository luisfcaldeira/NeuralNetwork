using MyNeuralNetwork.Domain.Entities.Nets.Generators;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.Networks;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Activations;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeuralNetwork.Domain.Tests.Nets.Networks
{
    public class NNGeneratorTests
    {
        NNGenerator _nNGenerator;
        NeuralNetwork _neuralNetwork;
        int[] formatLayers = new int[] { 12, 44, 56 };

        [SetUp]
        public void Setup()
        {
            _nNGenerator = new NNGenerator(new NeuronGenerator());
            _neuralNetwork = _nNGenerator.GenerateDefault(formatLayers);
        }

        [Test]
        public void TestIfGenerateCorrectNumberOfLayers()
        {
            Assert.That(_neuralNetwork.Layers.Count, Is.EqualTo(3));
        }

        [Test]
        public void TestIfGenerateCorrectNumberOfNeurons()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_neuralNetwork.Layers[0].Neurons.Count, Is.EqualTo(formatLayers[0]));
                Assert.That(_neuralNetwork.Layers[1].Neurons.Count, Is.EqualTo(formatLayers[1]));
            });
        }

        [Test]
        public void TestIfGenerateNextLayerCorrectly()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_neuralNetwork.Layers[0].NextLayer.Label, Is.EqualTo(1));
                Assert.That(_neuralNetwork.Layers[1].NextLayer.Label, Is.EqualTo(2));
                Assert.That(_neuralNetwork.Layers[2].NextLayer, Is.EqualTo(null));
            });
        }

        [Test]
        public void TestIfGeneratePreviousLayerCorrectly()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_neuralNetwork.Layers[0].PreviousLayer, Is.EqualTo(null));
                Assert.That(_neuralNetwork.Layers[1].PreviousLayer.Label, Is.EqualTo(0));
                Assert.That(_neuralNetwork.Layers[2].PreviousLayer.Label, Is.EqualTo(1));
            });
        }

        [Test]
        public void TestIfCreateSynapsesCorrectly()
        {
            int expectedTotalOfSynapses = 0;
            for(int i = 0; i < _neuralNetwork.Layers.Count - 1; i++)
            {
                int countNeuronsNextLayer = _neuralNetwork.Layers[i + 1].Neurons.Count;
                int countNeuronsThisLayer = _neuralNetwork.Layers[i].Neurons.Count;

                expectedTotalOfSynapses += countNeuronsNextLayer * countNeuronsThisLayer;
                TestContext.Write("Layer: ");
                TestContext.WriteLine(i + 1);
                    
                TestContext.Write("Count Neurons in this Layer: ");
                TestContext.WriteLine(countNeuronsThisLayer);

                TestContext.Write("Count Neurons in next Layer: ");
                TestContext.WriteLine(countNeuronsNextLayer);

                _neuralNetwork.Layers[i].Neurons.ForEach(neuron =>
                {
                    Assert.That(neuron.Synapses.Count(), Is.EqualTo(countNeuronsNextLayer));
                });
            }

            var totalOfSynapses = _neuralNetwork.Layers.SelectMany(l => l.Neurons).Select(x => x.Synapses.Count()).Sum();

            TestContext.Write("Expected total of Synapses: ");
            TestContext.WriteLine(expectedTotalOfSynapses);
            
            TestContext.Write("Total of Synapses: ");
            TestContext.WriteLine(totalOfSynapses);

            Assert.That(totalOfSynapses, Is.EqualTo(expectedTotalOfSynapses));
        }

        [Test]
        public void TestIfNumberOfNeuronsIsRight()
        {
            var quantity = 0;
            var rightNumberOfNeurons = 0;

            for(int i = 0; i < formatLayers.Length; i++)
            {
                rightNumberOfNeurons += formatLayers[i];
            }

            _neuralNetwork.Layers.ForEach((layer) =>
            {
                layer.Neurons.ForEach(neuron =>
                {
                    quantity++;
                });
            });

            Assert.That(quantity, Is.EqualTo(rightNumberOfNeurons));
        }

        [Test]
        public void TestIfCreateWeightsCorrectly()
        {
            var neuronGenerator = new NeuronGenerator();
            var min = 0;
            var max = 1;
            neuronGenerator.WeightConfiguration.SetMaxAndMin(min, max);

            Assert.That(neuronGenerator.WeightConfiguration.MinimumRange, Is.EqualTo(min));
            Assert.That(neuronGenerator.WeightConfiguration.MaximumRange, Is.EqualTo(max));

            var nNGen = new NNGenerator(neuronGenerator);

            var neuronNetwork = nNGen.GenerateDefault(new int[] { 1, 1 });

            var firstNeuron = neuronNetwork.Layers[0].Neurons[0];

            TestContext.WriteLine($"Weight: {firstNeuron.Synapses.Synapses[0].Weight}");

            Assert.That(firstNeuron.Synapses.Synapses[0].Weight.Value, Is.GreaterThan(min));
            Assert.That(firstNeuron.Synapses.Synapses[0].Weight.Value, Is.LessThan(max));
        }


        [Test]
        public void TestIfCreateBiasCorrectly()
        {
            var neuronGenerator = new NeuronGenerator();
            var min = 0;
            var max = 1;
            neuronGenerator.BiasConfiguration.SetMaxAndMin(min, max);

            Assert.That(neuronGenerator.BiasConfiguration.MinimumRange, Is.EqualTo(min));
            Assert.That(neuronGenerator.BiasConfiguration.MaximumRange, Is.EqualTo(max));

            var nNGen = new NNGenerator(neuronGenerator);

            var neuronNetwork = nNGen.GenerateDefault(new int[] { 1, 1 });

            var firstNeuron = neuronNetwork.Layers[0].Neurons[0];

            TestContext.WriteLine($"Weight: {firstNeuron.Bias}");

            Assert.That(firstNeuron.Bias, Is.GreaterThan(min));
            Assert.That(firstNeuron.Bias, Is.LessThan(max));
        }

    }
}
