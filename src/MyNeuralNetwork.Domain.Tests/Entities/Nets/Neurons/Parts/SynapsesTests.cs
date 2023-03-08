using MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics;
using MyNeuralNetwork.Domain.Entities.Nets.Generators;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Activations;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts;
using NUnit.Framework;

namespace MyNeuralNetwork.Domain.Tests.Entities.Nets.Neurons.Parts
{
    public class SynapsesTests
    {
        [Test]
        public void TestIfOutputIsCorrectly()
        {
            NeuronGenerator neuronGenerator = new NeuronGenerator();
            neuronGenerator.WeightConfiguration.SetMaxAndMin(1, 1);

            var nNgen = new NNGenerator(neuronGenerator);

            var neuralNetwork = nNgen.GenerateDefault(new int[] { 1, 1 });

            neuralNetwork.Predict(new Input[] { new Input(1) });

            var value1 = neuralNetwork.Layers[0].Neurons[0].Synapses.GetOutput(neuralNetwork.Layers[1].Neurons[0]);
            //var value2 = neuralNetwork.Layers[1].Neurons[0].Value;

            Assert.That(value1.Value, Is.EqualTo(1));
            //Assert.That(value2, Is.EqualTo(1));

        }

    }
}
