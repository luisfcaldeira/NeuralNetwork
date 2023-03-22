using MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics;
using MyNeuralNetwork.Domain.Entities.Nets.Collections.Neurons;
using MyNeuralNetwork.Domain.Entities.Nets.Interfaces.Neurons;
using MyNeuralNetwork.Domain.Entities.Nets.Interfaces.Neurons.Activations;
using MyNeuralNetwork.Domain.Entities.Nets.Interfaces.Neurons.Parts;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts;
using System;

namespace MyNeuralNetwork.Domain.Entities.Nets.Neurons
{

    public class NeuronGenerator : INeuronGenerator
    {
        public RangeConfiguration WeightConfiguration { get; set; } = new RangeConfiguration();
        public RangeConfiguration BiasConfiguration { get; set; } = new RangeConfiguration();
        public double LearningRate { get; set; } = Neuron.DeafultLearningRate;

        public NeuronCollection Generate<ISynapseManagerImplementation, IActivatorType>(int quantity) 
        {
            var activator = Activator.CreateInstance(typeof(IActivatorType)) as IActivator;
            return Generate< ISynapseManagerImplementation >(() => activator, quantity);
        }

        public NeuronCollection Generate<ISynapseManagerImplementation>(int quantity, IActivator activator)
        {
            return Generate<ISynapseManagerImplementation>(() => activator, quantity);
        }

        private NeuronCollection Generate<T>(Func<IActivator> func, int quantity)
        {
            var neurons = new NeuronCollection();
            for (var i = 0; i < quantity; i++)
            {
                var synapseManager = Activator.CreateInstance(typeof(T)) as ISynapseManager;
                var activator = func.Invoke();

                synapseManager.WeightConfiguration.MaximumRange = WeightConfiguration.MaximumRange;
                synapseManager.WeightConfiguration.MinimumRange = WeightConfiguration.MinimumRange;

                RandomDoubleValue bias = new RandomDoubleValue(BiasConfiguration.MinimumRange, BiasConfiguration.MaximumRange);
                neurons.Add(new Neuron(activator, bias, synapseManager) { LearningRate = LearningRate });
            }
            return neurons;
        }
    }
}
