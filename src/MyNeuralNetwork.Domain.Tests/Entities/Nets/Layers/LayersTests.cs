using MyNeuralNetwork.Domain.Entities.Nets.Collections.Neurons;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.Layers;
using MyNeuralNetwork.Tests.Utils;
using NUnit.Framework;

namespace MyNeuralNetwork.Domain.Tests.Entities.Nets.Layers
{
    public class LayersTests
    {
        
        [Test]
        public void TestOutput()
        {
            NeuronCollection neuronCollection = new NeuronCollection
            {
                NeuronGenerator.MakeEmptyNeuron(), 
                NeuronGenerator.MakeEmptyNeuron(), 
            };

            var layerCounter = new LayerCounter();
            var layer = new Layer(layerCounter, neuronCollection);
            layer.Add(new Input[] 
            { 
                new Input(0.5f),
                new Input(1),
            });

            layer.UpdateOutput();

            Assert.That(layer.Output[0], Is.EqualTo(0.5));
            Assert.That(layer.Output[1], Is.EqualTo(1));
        }
    }
}
