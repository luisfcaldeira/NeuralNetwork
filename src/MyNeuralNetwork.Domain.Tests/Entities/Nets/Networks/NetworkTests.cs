using MyNeuralNetwork.Domain.Entities.Nets.Collections.IO.Outputs;
using MyNeuralNetwork.Domain.Entities.Nets.Generators;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Outputs;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts;
using MyNeuralNetwork.Tests.Utils.Activations;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeuralNetwork.Domain.Tests.Entities.Nets.Networks
{
    public class NetworkTests
    {
        [Test]
        public void TestIfValuePassCorrectlyThroughNetwork()
        {
            var neuronGenerator = new NeuronGenerator();
            neuronGenerator.WeightConfiguration.SetMaxAndMin(1, 1);
            neuronGenerator.BiasConfiguration.SetMaxAndMin(0, 0);

            var nNGenerator = new NNGenerator(neuronGenerator);
            var network = nNGenerator.Generate<SynapseManager, ActivationTester>(new int[] { 1, 1, 1 });

            var result = network.Predict(new Input[] 
            { 
                new Input(1),
            });

            Assert.That(result[0], Is.EqualTo(1));
        }
    }
}
