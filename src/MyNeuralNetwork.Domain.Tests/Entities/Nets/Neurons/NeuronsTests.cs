using MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics;
using MyNeuralNetwork.Domain.Entities.Nets.Collections.Neurons;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Outputs;
using MyNeuralNetwork.Domain.Entities.Nets.Layers;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Activations;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts;
using NUnit.Framework;

namespace MyNeuralNetwork.Domain.Tests.Nets.Neurons
{
    public class NeuronsTests
    {
        Neuron _neuron;
        [SetUp]
        public void Setup()
        {
            Tanh activation = new Tanh();
            _neuron = new Neuron(activation, new RandomFloatValue(), new SynapseManager());
        }

        [Test]
        public void TestIfFeedbackBringRightValueForGamma()
        {

            float value1 = 1f;
            float value2 = 0.5f;
            var output = new Output(value1);
            var expected = new Expected(value2);

            _neuron.UpdateGamma(new Feedback(output, expected));

            Assert.That(_neuron.Gamma, Is.EqualTo(value1 - value2));
        }
    }
}