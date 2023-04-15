using MyNeuralNetwork.Domain.Dtos.Entities.Nets.Neurons;
using MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics;
using MyNeuralNetwork.Domain.Entities.Nets.Collections.Neurons;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts;
using MyNeuralNetwork.Domain.Interfaces.Neurons;
using MyNeuralNetwork.Domain.Interfaces.Neurons.Activations;
using MyNeuralNetwork.Domain.Interfaces.Neurons.Parts;
using System;
using System.Collections.Generic;

namespace MyNeuralNetwork.Domain.Entities.Nets.Neurons
{

    public class NeuronGenerator : INeuronGenerator
    {
        public RangeConfiguration WeightConfiguration { get; set; } = new RangeConfiguration();
        public RangeConfiguration BiasConfiguration { get; set; } = new RangeConfiguration();
        public double LearningRate { get; set; } = Neuron.DeafultLearningRate;

        public NeuronCollection Generate<ISynapseManagerImplementation, IActivatorType>(int quantity)
        {
            var activator = InstantiateActivator(typeof(IActivatorType));
            return Generate<ISynapseManagerImplementation>(() => activator, quantity);
        }

        public NeuronCollection Generate<ISynapseManagerImplementation>(int quantity, IActivator activator)
        {
            return Generate<ISynapseManagerImplementation>(() => activator, quantity);
        }

        public NeuronCollection Generate<ISynapseManagerImplementation>(List<NeuronDto> neuronsDto)
        {
            var neuronCollection = new NeuronCollection();
            foreach (var neuronDto in neuronsDto)
            {
                var activator = InstantiateActivator(neuronDto.Activator);
                var synapseManager = InstantiateSynapseManager<ISynapseManagerImplementation>();

                var neuron = GenerateNeuron(activator, synapseManager, new NeuralDoubleValue(neuronDto.Bias), neuronDto.LearningRate);
                neuronCollection.Add(neuron);
            }

            return neuronCollection;
        }

        private static IActivator InstantiateActivator(string activatorTypeName)
        {
            var type = Type.GetType(activatorTypeName);
            return InstantiateActivator(type);
        }

        private static IActivator InstantiateActivator(Type type)
        {
            return Activator.CreateInstance(type) as IActivator;
        }

        private NeuronCollection Generate<T>(Func<IActivator> func, int quantity)
        {
            var neurons = new NeuronCollection();
            for (var i = 0; i < quantity; i++)
            {
                var activator = func.Invoke();
                ISynapseManager synapseManager = InstantiateSynapseManager<T>();

                Neuron neuron = ConfigureAndGenerateNeuron(activator, synapseManager);

                neurons.Add(neuron);
            }
            return neurons;
        }

        private static ISynapseManager InstantiateSynapseManager<T>()
        {
            return Activator.CreateInstance(typeof(T)) as ISynapseManager;
        }

        private Neuron ConfigureAndGenerateNeuron(IActivator activator, ISynapseManager synapseManager)
        {
            synapseManager.WeightConfiguration.MaximumRange = WeightConfiguration.MaximumRange;
            synapseManager.WeightConfiguration.MinimumRange = WeightConfiguration.MinimumRange;

            NeuralDoubleValue bias = new RandomDoubleValue(BiasConfiguration.MinimumRange, BiasConfiguration.MaximumRange);

            return GenerateNeuron(activator, synapseManager, bias, LearningRate);
        }

        private static Neuron GenerateNeuron(IActivator activator, ISynapseManager synapseManager, NeuralDoubleValue bias, double learningRate)
        {
            return new Neuron(activator, bias, synapseManager) { LearningRate = learningRate };
        }
    }
}
