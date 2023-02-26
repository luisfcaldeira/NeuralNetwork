using MyNeuralNetwork.Domain.Entities.Nets.Collections.Neurons;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Outputs;
using MyNeuralNetwork.Domain.Entities.Nets.Layers;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Activations;
using NUnit.Framework;

namespace MyNeuralNetwork.Domain.Tests.Nets.Neurons
{
    public class NeuronTests
    {
        Neuron _neuron;
        [SetUp]
        public void Setup()
        {
            _neuron = new Neuron(new Tanh());
        }

        [Test]
        public void TestIfFeedbackBringRightValueForGamma()
        {

            float value1 = 1f;
            float value2 = 0.5f;
            var output = new Output(value1);
            var expected = new Expected(value2);

            _neuron.UpdateGamma(new Feedback(output, expected));

            Assert.That(_neuron.Gamma.Value, Is.EqualTo(value1 - value2));
        }
    }
}