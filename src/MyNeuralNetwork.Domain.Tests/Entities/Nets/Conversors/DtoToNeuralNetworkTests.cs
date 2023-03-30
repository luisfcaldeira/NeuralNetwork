using MyNeuralNetwork.Domain.Dtos.Entities.Nets.Layers;
using MyNeuralNetwork.Domain.Dtos.Entities.Nets.Networks;
using MyNeuralNetwork.Domain.Dtos.Entities.Nets.Neurons;
using MyNeuralNetwork.Domain.Dtos.Entities.Nets.Neurons.Parts;
using MyNeuralNetwork.Domain.Entities.Nets.Conversors;
using MyNeuralNetwork.Domain.Entities.Nets.Generators.Supports;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Activations;
using NUnit.Framework;
using System.Collections.Generic;
using System.Reflection;

namespace MyNeuralNetwork.Domain.Tests.Entities.Nets.Conversors
{
    public class DtoToNeuralNetworkTests
    {
        [Test]
        public void TestIfItThrowAnException()
        {
            NeuralNetworkDto dto = GenerateDto();

            var dtoToNeuralNetwork = new DtoToNeuralNetwork(new LayersLinker());
            var type = dtoToNeuralNetwork.GetType();
            var method = type.GetMethod("CheckLayerLabel", BindingFlags.NonPublic | BindingFlags.Static);
            var layerDto = new LayerDto()
            {
                Label = 5,
            };

            object[] parameters = new object[] { 1, layerDto };

            Assert.That(method, Is.Not.Null);
            Assert.Throws<TargetInvocationException>(() =>
            {
                method.Invoke(dtoToNeuralNetwork, parameters);
            });

            try
            {
                method.Invoke(dtoToNeuralNetwork, parameters);
            }
            catch (TargetInvocationException ex)
            {
                Assert.That(ex.InnerException.Message, Is.EqualTo($"Layers doesn't match. It should be #1 but #5 was found."));
            }
        }

        [Test]
        public static void TestIfLayersAreConverted()
        {
            var dto = GenerateDto();
            var dtoToNeuralNetwork = new DtoToNeuralNetwork(new LayersLinker());
            var result = dtoToNeuralNetwork.GenerateLayerCollection(dto);

            Assert.That(result[0].Label, Is.EqualTo(0));
            Assert.That(result[1].Label, Is.EqualTo(1));

            Assert.That(result[0].Neurons[0].Bias, Is.EqualTo(0.55));
            Assert.That(result[1].Neurons[0].Bias, Is.EqualTo(0.44));

            Assert.That(result[0].Neurons[0].Synapses.GetSynapse(result[1].Neurons[0]).Weight.Value, Is.EqualTo(0.789));
        }

        private static NeuralNetworkDto GenerateDto()
        {
            int targetIndex = 2;
            var layer1 = new LayerDto()
            {
                Label = 0,
                Neurons = new List<NeuronDto>()
                        {
                            new NeuronDto()
                            {
                                Index = 1,
                                Activator = GetActivatorFullName(),
                                Bias = 0.55,
                                LearningRate = 0.1,
                                Synapses = new SynapseManagerDto()
                                {
                                    Synapses = new List<SynapseDto> {
                                        new SynapseDto()
                                        {
                                            Weight = 0.789,
                                            TargetGuid = targetIndex,
                                        }
                                    }
                                }
                            }
                        }
            };

            var layer2 = new LayerDto()
            {
                Label = 1,
                Neurons = new List<NeuronDto>()
                        {
                            new NeuronDto()
                            {
                                Index = targetIndex,
                                Activator = GetActivatorFullName(),
                                Bias = 0.44,
                                LearningRate = 0.1,
                            }
                        }
            };

            var dto = new NeuralNetworkDto()
            {
                Layers = new List<LayerDto>()
                {
                    layer1,
                    layer2
                }
            };
            return dto;
        }

        private static string GetActivatorFullName()
        {
            return typeof(Tanh).FullName;
        }
    }
}
