using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.Layers;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts;
using MyNeuralNetwork.Tests.Utils.Activations;
using NUnit.Framework;

namespace MyNeuralNetwork.Domain.Tests.Entities.Nets.Neurons.Parts
{
    public class SynapsesTests
    {
        [Test]
        public void TestIfItSumWeightsCorrectly()
        {
            int weightValue = 2;
            var neuronGenerator = new NeuronGenerator();
            neuronGenerator.WeightConfiguration.SetMaxAndMin(weightValue, weightValue);
            float biasValue = 1f;
            neuronGenerator.BiasConfiguration.SetMaxAndMin(biasValue, biasValue);

            var layerCounter = new LayerCounter();
            var layer1 = new Layer(layerCounter, neuronGenerator.Generate<SynapseManager, ActivationTester>(5));
            var layer2 = new Layer(layerCounter, neuronGenerator.Generate<SynapseManager, ActivationTester>(5));
            var commitMethod = layer2.Neurons[0].GetType().GetMethod("Commit", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            Assert.That(commitMethod, Is.Not.Null);

            layer1.NextLayer = layer2;
            layer2.PreviousLayer = layer1;

            Input input1 = new Input(1);
            Input input2 = new Input(2);
            Input input3 = new Input(3);
            Input input4 = new Input(4);
            Input input5 = new Input(5);

            layer1.Add(new Input[]
            {
                input1,
                input2,
                input3,
                input4,
                input5,
            });

            Assert.Multiple(() =>
            {
                Assert.That(layer1.Neurons[0].Value.Value, Is.EqualTo(input1.Value));
                Assert.That(layer1.Neurons[1].Value.Value, Is.EqualTo(input2.Value));
                Assert.That(layer1.Neurons[2].Value.Value, Is.EqualTo(input3.Value));
                Assert.That(layer1.Neurons[3].Value.Value, Is.EqualTo(input4.Value));
                Assert.That(layer1.Neurons[4].Value.Value, Is.EqualTo(input5.Value));
            });

            layer1.Neurons[0].Synapses.TransmitTo(layer2.Neurons[0]);
            commitMethod.Invoke(layer2.Neurons[0], null);

            Assert.That(layer2.Neurons[0].Value.Value, Is.EqualTo(input1.Value * weightValue + biasValue));

            layer1.Neurons[0].Synapses.TransmitTo(layer2.Neurons[0]);
            layer1.Neurons[1].Synapses.TransmitTo(layer2.Neurons[0]);
            commitMethod.Invoke(layer2.Neurons[0], null);

            Assert.That(layer2.Neurons[0].Value.Value, Is.EqualTo((input1.Value + input2.Value) * weightValue + biasValue));

            layer1.Neurons[0].Synapses.TransmitTo(layer2.Neurons[0]);
            layer1.Neurons[1].Synapses.TransmitTo(layer2.Neurons[0]);
            layer1.Neurons[2].Synapses.TransmitTo(layer2.Neurons[0]);
            commitMethod.Invoke(layer2.Neurons[0], null);

            Assert.That(layer2.Neurons[0].Value.Value, Is.EqualTo((input1.Value + input2.Value + input3.Value) * weightValue + biasValue));

            layer1.Neurons[0].Synapses.TransmitTo(layer2.Neurons[0]);
            layer1.Neurons[1].Synapses.TransmitTo(layer2.Neurons[0]);
            layer1.Neurons[2].Synapses.TransmitTo(layer2.Neurons[0]);
            layer1.Neurons[3].Synapses.TransmitTo(layer2.Neurons[0]);
            commitMethod.Invoke(layer2.Neurons[0], null);

            Assert.That(layer2.Neurons[0].Value.Value, Is.EqualTo((input1.Value + input2.Value + input3.Value + input4.Value) * weightValue + biasValue));

            layer1.Neurons[0].Synapses.TransmitTo(layer2.Neurons[0]);
            layer1.Neurons[1].Synapses.TransmitTo(layer2.Neurons[0]);
            layer1.Neurons[2].Synapses.TransmitTo(layer2.Neurons[0]);
            layer1.Neurons[3].Synapses.TransmitTo(layer2.Neurons[0]);
            layer1.Neurons[4].Synapses.TransmitTo(layer2.Neurons[0]);
            commitMethod.Invoke(layer2.Neurons[0], null);

            Assert.That(layer2.Neurons[0].Value.Value, Is.EqualTo((input1.Value + input2.Value + input3.Value + input4.Value + input5.Value) * weightValue + biasValue));
        }
    }
}
