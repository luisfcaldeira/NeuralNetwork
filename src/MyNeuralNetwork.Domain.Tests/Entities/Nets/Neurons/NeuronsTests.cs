using MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Outputs;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Activations;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts;
using MyNeuralNetwork.Tests.Utils.Activations;
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
        public void TestIfValuePassThroughNeuron()
        {
            Neuron neuron = MakeANeuron();
            neuron.Feed(new Input(1));
            Assert.That(neuron.Value.Value, Is.EqualTo(1));
        }

        private static Neuron MakeANeuron()
        {
            return new Neuron(new ActivationTester(), new RandomFloatValue(), new SynapseManager());
        }

        [Test]
        public void TestUpdateGamma()
        {
            var neuron = MakeANeuron();

            var method = neuron.GetType().GetMethod("UpdateGamma", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            neuron.Feed(new Input(1));

            Assert.That(method != null, Is.True);
            
            method.Invoke(neuron, new object[] { new NeuralFloatValue(2) });

            Assert.That(neuron.Gamma, Is.EqualTo(2));
        }

    }
}