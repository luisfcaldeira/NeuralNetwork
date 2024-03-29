using MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts;
using MyNeuralNetwork.Tests.Utils.Activations;
using NUnit.Framework;

namespace MyNeuralNetwork.Domain.Tests.Nets.Neurons
{
    public class NeuronsTests
    {
        [Test]
        public void TestIfValuePassThroughNeuron()
        {
            Neuron neuron = MakeANeuron();
            neuron.Feed(new Input(1));
            Assert.That(neuron.Value.Value, Is.EqualTo(1));
        }

        private static Neuron MakeANeuron()
        {
            return new Neuron(new ActivationTester(), new RandomDoubleValue(), new SynapseManager());
        }

        [Test]
        public void TestUpdateGamma()
        {
            var neuron = MakeANeuron();

            var method = neuron.GetType().GetMethod("UpdateGamma", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            neuron.Feed(new Input(1));

            Assert.That(method != null, Is.True);

            method.Invoke(neuron, new object[] { new NeuralDoubleValue(2) });

            Assert.That(neuron.Gamma, Is.EqualTo(2));
        }

        [Test]
        public void TestIfFeedWithBias()
        {
            var neuron = MakeANeuron();

            float bias = 2;
            float trasmition = 3;

            neuron.Bias = bias;

            var method = neuron.GetType().GetMethod("Transmit", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            method.Invoke(neuron, new object[] { new Transmition(trasmition) });

            var methodCommit = neuron.GetType().GetMethod("Commit", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            methodCommit.Invoke(neuron, null);
            var value = neuron.Value.Value;

            TestContext.Write("Neuron's value: ");
            TestContext.WriteLine(value);

            Assert.That(value, Is.EqualTo(bias + trasmition));
        }

        [Test]
        public void TestIfItMutates()
        {
            var neuron = MakeANeuron();
            var neuron2 = new Neuron(new ActivationTester(), new NeuralDoubleValue(0.5), new SynapseManager());

            var method = neuron.GetType().GetMethod("Mutate", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            Assert.That(method, Is.Not.Null);

            Assert.That(neuron.Bias, Is.Not.EqualTo(0.5));

            method.Invoke(neuron, new object[] { neuron2, 0, 0 });

            Assert.That(neuron.Bias, Is.EqualTo(0.5));
        }
    }
}