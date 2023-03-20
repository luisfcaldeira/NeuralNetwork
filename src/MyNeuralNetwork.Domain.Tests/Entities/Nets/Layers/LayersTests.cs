using MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics;
using MyNeuralNetwork.Domain.Entities.Nets.Collections.Neurons;
using MyNeuralNetwork.Domain.Entities.Nets.Interfaces.Neurons.Activations;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.Layers;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts;
using MyNeuralNetwork.Tests.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeuralNetwork.Domain.Tests.Entities.Nets.Layers
{
    public class LayersTests
    {
        
        [Test]
        public void TestOutput()
        {
            NeuronCollection neuronCollection = new NeuronCollection
            {
                NeuronGeneratorForTests.MakeEmptyNeuron(), 
                NeuronGeneratorForTests.MakeEmptyNeuron(), 
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
