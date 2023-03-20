using MyNeuralNetwork.Domain.Entities.Nets.Generators;
using MyNeuralNetwork.Domain.Entities.Nets.Interfaces.Neurons.Activations;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Outputs;
using MyNeuralNetwork.Domain.Entities.Nets.Layers;
using MyNeuralNetwork.Domain.Entities.Nets.Networks;
using MyNeuralNetwork.Domain.Entities.Nets.Networks.Circuits.Backward;
using MyNeuralNetwork.Domain.Entities.Nets.Networks.Circuits.Backward.Support;
using MyNeuralNetwork.Domain.Entities.Nets.Networks.Circuits.Forward;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts;
using MyNeuralNetwork.Domain.Tests.Entities.Support;
using MyNeuralNetwork.Tests.Utils.Activations;
using NUnit.Framework;
using System.Linq;

namespace MyNeuralNetwork.Domain.Tests.Entities.Nets.Circuits.Backward
{
    public class BackpropagationTests
    {
        FeedForward _feedFoward = new();
        Backpropagation _backpropagation = new();
        Expected expected = new Expected(0.5f);
        Input input = new Input(1);
        IActivator _activator = new ActivationTester();

      
        private NeuralNetwork GenerateNN(int[] format)
        {
            var ngen = new NeuronGenerator();
            ngen.WeightConfiguration.SetMaxAndMin(1, 1);
            ngen.BiasConfiguration.SetMaxAndMin(0, 0);
            var nngen = new NNGenerator(ngen);
            var neuralNetwork = nngen.Generate<SynapseManager, ActivationTester>(format);
            _feedFoward.Send(neuralNetwork, new Input[] { input });

            return neuralNetwork;
        }

        [Test]
        public void TestIfItIsSetDefaultValues()
        {
            var neuralNetwork = GenerateNN(new int[] { 1, 1, 1 });
            var testedNeuron = neuralNetwork.Layers[1].Neurons[0];
            var lastNeuron = neuralNetwork.Layers[2].Neurons[0];
            var previousWeight = 1;
            var previousBias = 0;
            var previousGamma = 0;

            PropertiesTester.TestWeight(testedNeuron, lastNeuron, previousWeight);
            PropertiesTester.TestBias(testedNeuron, previousBias);
            PropertiesTester.TestGamma(testedNeuron, previousGamma);
            PropertiesTester.TestValue(testedNeuron, 1);
        }

        [Test]
        public void TestIfIOutputNeuronGammaIsUpdated()
        {
            var neuralNetwork = GenerateNN(new int[] { 1, 1, 1 });
            var testedNeuron = neuralNetwork.Layers[2].Neurons[0];

            Layer lastLayer = neuralNetwork.Layers.Last();
            LastLayerFiller.UpdateLayerIfItsLastOne(new Expected[] { new Expected(0.5f) }, lastLayer);

            var expectedGamma = (testedNeuron.Value.Value - expected.Value) * _activator.Derivative(testedNeuron.Value.Value);

            PropertiesTester.TestGamma(testedNeuron, expectedGamma);
        }

        [Test]
        public void TestIfItFillsHiddenLayers()
        {
            var neuralNetwork = GenerateNN(new int[] { 1, 1, 1 });
            var testedNeuron = neuralNetwork.Layers[1].Neurons[0];
            var lastNeuron = neuralNetwork.Layers[2].Neurons[0];
            var previousWeight = 1;
            var previousBias = 0;

            HiddenLayerFiller.UpdateValuesIfTheresANextLayer(neuralNetwork.Layers[1]);

            float lastNeuronGamma = lastNeuron.Gamma;
            float neuronValue = testedNeuron.Value.Value;
            float learningRate = testedNeuron.LearningRate;

            float expectedWeight = previousWeight - lastNeuronGamma * neuronValue * learningRate;
            float expectedBias = previousBias - lastNeuronGamma * learningRate;
            float expectedGamma = lastNeuron.Gamma * testedNeuron.Synapses.GetWeightFor(lastNeuron).Value;

            PropertiesTester.TestWeight(testedNeuron, lastNeuron, expectedWeight);
            PropertiesTester.TestBias(testedNeuron, expectedBias);
            PropertiesTester.TestGamma(testedNeuron, expectedGamma);

        }
    }
}
