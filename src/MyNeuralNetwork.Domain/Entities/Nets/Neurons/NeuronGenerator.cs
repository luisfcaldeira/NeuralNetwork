using MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics;
using MyNeuralNetwork.Domain.Entities.Nets.Collections.Neurons;
using MyNeuralNetwork.Domain.Entities.Nets.Interfaces.Neurons;
using MyNeuralNetwork.Domain.Entities.Nets.Interfaces.Neurons.Activations;
using MyNeuralNetwork.Domain.Entities.Nets.Interfaces.Neurons.Parts;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Activations;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts;
using System;

namespace MyNeuralNetwork.Domain.Entities.Nets.Neurons
{

    public class NeuronGenerator : INeuronGenerator
    {
        public RangeConfiguration WeightConfiguration { get; set; } = new RangeConfiguration();
        public RangeConfiguration BiasConfiguration { get; set; } = new RangeConfiguration();

        public NeuronCollection Generate<ISynapseManagerType, IActivatorType>(int quantity) 
        {
            var neurons = new NeuronCollection();
            for (var i = 0; i < quantity; i++)
            {
                var synapseManager = Activator.CreateInstance(typeof(ISynapseManagerType)) as ISynapseManager;
                var activator = Activator.CreateInstance(typeof(IActivatorType)) as IActivator;

                synapseManager.WeightConfiguration.MaximumRange = WeightConfiguration.MaximumRange;
                synapseManager.WeightConfiguration.MinimumRange = WeightConfiguration.MinimumRange;

                RandomFloatValue bias = new RandomFloatValue(BiasConfiguration.MinimumRange, BiasConfiguration.MaximumRange);
                neurons.Add(new Neuron(activator, bias, synapseManager));
            }
            return neurons;
        }
    }
}
