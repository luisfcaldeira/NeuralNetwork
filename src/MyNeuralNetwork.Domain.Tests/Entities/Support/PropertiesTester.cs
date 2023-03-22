using MyNeuralNetwork.Domain.Entities.Nets.Neurons;
using NUnit.Framework;

namespace MyNeuralNetwork.Domain.Tests.Entities.Support
{
    internal class PropertiesTester
    {

        public static void TestValue(Neuron neuron, float expectedValue)
        {
            TestContext.Write("Testing if value is ");
            TestContext.WriteLine(expectedValue);

            Assert.That(neuron.Value.Value, Is.EqualTo(expectedValue));
        }

        public static void TestBias(Neuron neuron, double expectBias)
        {
            TestContext.Write("Testing if bias is ");
            TestContext.WriteLine(expectBias);

            Assert.That(neuron.Bias, Is.EqualTo(expectBias));
        }

        public static void TestGamma(Neuron neuron, double expectedGamma)
        {
            TestContext.Write("Testing if gamma is ");
            TestContext.WriteLine(expectedGamma);

            Assert.That(neuron.Gamma, Is.EqualTo(expectedGamma));
        }

        public static void TestWeight(Neuron neuron, Neuron nextNeuron, double expectValue)
        {
            TestContext.Write("Testing if weight is ");
            TestContext.WriteLine(expectValue);

            var weight = neuron.Synapses.GetWeightFor(nextNeuron).Value;
            Assert.That(weight, Is.EqualTo(expectValue));
        }
    }
}
