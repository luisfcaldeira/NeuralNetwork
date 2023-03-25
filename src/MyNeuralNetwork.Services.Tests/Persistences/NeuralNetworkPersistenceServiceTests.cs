using AutoMapper;
using Core.Infra.IoC.Mappers;
using Core.Infra.Services.Persistences;
using MyNeuralNetwork.Domain.Dtos.Entities.Nets.Networks;
using MyNeuralNetwork.Domain.Entities.Nets.Networks;
using MyNeuralNetwork.Tests.Utils;
using NUnit.Framework;
using System.IO;
using System.Xml.Serialization;

namespace MyNeuralNetwork.Infra.Services.Tests.Persistences
{
    public class NeuralNetworkPersistenceServiceTests
    {
        [Test]
        public void TestIfCreateXml()
        {
            var service = GetService();

            var neuralNetwork = NetworkGeneratorForTests.GiveMeOne(new int[] { 1, 2, 3 });
            
            service.Save(neuralNetwork);

            var neuralNetworkDto = ReadResultInFile();

            TestQuantity(neuralNetwork, neuralNetworkDto);

            TestBias(neuralNetwork, neuralNetworkDto);

            TestWeights(neuralNetwork, neuralNetworkDto);
        }

        [Test]
        public void TestIfReadXml()
        {
            var service = GetService();

            var resultDto = service.Load();

            var expectedDto = ReadResultInFile();

            for(var layerCount = 0; layerCount < expectedDto.Layers.Count; layerCount++)
            {
                Assert.That(resultDto.Layers[layerCount].Label, Is.EqualTo(expectedDto.Layers[layerCount].Label));

                var resultLayer = resultDto.Layers[layerCount];
                var expectedLayer = expectedDto.Layers[layerCount];

                TestContext.Write("Result layer label #");
                TestContext.WriteLine(resultLayer.Label);

                TestContext.Write("Expected layer label #");
                TestContext.WriteLine(expectedLayer.Label);

                for (var neuronCount = 0; neuronCount < expectedLayer.Neurons.Count; neuronCount++)
                {
                    var resultNeuron = resultLayer.Neurons[neuronCount];
                    var expectedNeuron = expectedLayer.Neurons[neuronCount];

                    TestContext.WriteLine($"Result neuron #{neuronCount}");
                    TestContext.WriteLine($"Expected neuron #{neuronCount}");
                    TestContext.WriteLine($"Result bias: {resultNeuron.Bias} | Expected bias: {expectedNeuron.Bias}");

                    Assert.That(resultNeuron.Bias, Is.EqualTo(expectedNeuron.Bias));

                    for (var i = 0; i < resultNeuron.Synapses.Synapses.Count; i++)
                    {
                        var resultWeight = resultNeuron.Synapses.Synapses[i].Weight;
                        var expectedWeight = expectedNeuron.Synapses.Synapses[i].Weight;

                        TestContext.WriteLine($"Result weight: {resultWeight} | Expected weight: {expectedWeight}");
                    }
                }
            }
        }

        private static NeuralNetworkDto ReadResultInFile()
        {
            var service = GetService(); 
            var serializer = new XmlSerializer(typeof(NeuralNetworkDto));
            TextReader reader = new StreamReader(service.FullPath);
            var neuralNetworkDto = (NeuralNetworkDto)serializer.Deserialize(reader);
            return neuralNetworkDto;
        }

        private static void TestWeights(NeuralNetwork neuralNetwork, NeuralNetworkDto neuralNetworkDto)
        {
            Assert.Multiple(() =>
            {
                Assert.That(neuralNetworkDto.Layers[0].Neurons[0].Synapses.Synapses[0].Weight, Is.EqualTo(neuralNetwork.Layers[0].Neurons[0].Synapses.GetWeightFor(neuralNetwork.Layers[1].Neurons[0]).Value));
                Assert.That(neuralNetworkDto.Layers[0].Neurons[0].Synapses.Synapses[1].Weight, Is.EqualTo(neuralNetwork.Layers[0].Neurons[0].Synapses.GetWeightFor(neuralNetwork.Layers[1].Neurons[1]).Value));

                Assert.That(neuralNetworkDto.Layers[1].Neurons[0].Synapses.Synapses[0].Weight, Is.EqualTo(neuralNetwork.Layers[1].Neurons[0].Synapses.GetWeightFor(neuralNetwork.Layers[2].Neurons[0]).Value));
                Assert.That(neuralNetworkDto.Layers[1].Neurons[0].Synapses.Synapses[1].Weight, Is.EqualTo(neuralNetwork.Layers[1].Neurons[0].Synapses.GetWeightFor(neuralNetwork.Layers[2].Neurons[1]).Value));
                Assert.That(neuralNetworkDto.Layers[1].Neurons[0].Synapses.Synapses[2].Weight, Is.EqualTo(neuralNetwork.Layers[1].Neurons[0].Synapses.GetWeightFor(neuralNetwork.Layers[2].Neurons[2]).Value));

                Assert.That(neuralNetworkDto.Layers[1].Neurons[1].Synapses.Synapses[0].Weight, Is.EqualTo(neuralNetwork.Layers[1].Neurons[1].Synapses.GetWeightFor(neuralNetwork.Layers[2].Neurons[0]).Value));
                Assert.That(neuralNetworkDto.Layers[1].Neurons[1].Synapses.Synapses[1].Weight, Is.EqualTo(neuralNetwork.Layers[1].Neurons[1].Synapses.GetWeightFor(neuralNetwork.Layers[2].Neurons[1]).Value));
                Assert.That(neuralNetworkDto.Layers[1].Neurons[1].Synapses.Synapses[2].Weight, Is.EqualTo(neuralNetwork.Layers[1].Neurons[1].Synapses.GetWeightFor(neuralNetwork.Layers[2].Neurons[2]).Value));
            });
        }

        private static void TestQuantity(NeuralNetwork neuralNetwork, NeuralNetworkDto neuralNetworkDto)
        {
            Assert.Multiple(() =>
            {
                Assert.That(neuralNetworkDto.Layers, Has.Count.EqualTo(neuralNetwork.Layers.Count));
                Assert.That(neuralNetworkDto.Layers[0].Neurons, Has.Count.EqualTo(neuralNetwork.Layers[0].Neurons.Count));
                Assert.That(neuralNetworkDto.Layers[1].Neurons, Has.Count.EqualTo(neuralNetwork.Layers[1].Neurons.Count));
                Assert.That(neuralNetworkDto.Layers[2].Neurons, Has.Count.EqualTo(neuralNetwork.Layers[2].Neurons.Count));
            });
        }

        private static void TestBias(NeuralNetwork neuralNetwork, NeuralNetworkDto neuralNetworkDto)
        {
            Assert.Multiple(() =>
            {
                Assert.That(neuralNetworkDto.Layers[0].Neurons[0].Bias, Is.EqualTo(neuralNetwork.Layers[0].Neurons[0].Bias));
                Assert.That(neuralNetworkDto.Layers[1].Neurons[0].Bias, Is.EqualTo(neuralNetwork.Layers[1].Neurons[0].Bias));
                Assert.That(neuralNetworkDto.Layers[1].Neurons[1].Bias, Is.EqualTo(neuralNetwork.Layers[1].Neurons[1].Bias));
                Assert.That(neuralNetworkDto.Layers[2].Neurons[0].Bias, Is.EqualTo(neuralNetwork.Layers[2].Neurons[0].Bias));
                Assert.That(neuralNetworkDto.Layers[2].Neurons[1].Bias, Is.EqualTo(neuralNetwork.Layers[2].Neurons[1].Bias));
                Assert.That(neuralNetworkDto.Layers[2].Neurons[2].Bias, Is.EqualTo(neuralNetwork.Layers[2].Neurons[2].Bias));
            });
        }

        private static NeuralNetworkPersistenceService GetService()
        {
            var mapper = new IocMapper();
            var service = new NeuralNetworkPersistenceService(mapper.GetService<IMapper>());
            return service;
        }
    }
}
