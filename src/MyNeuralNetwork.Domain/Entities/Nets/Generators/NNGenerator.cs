using MyNeuralNetwork.Domain.Entities.Nets.Layers;
using MyNeuralNetwork.Domain.Entities.Nets.Networks;
using MyNeuralNetwork.Domain.Entities.Nets.Networks.Circuits.Forward;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Activations;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts;
using MyNeuralNetwork.Domain.Interfaces.Generators;
using MyNeuralNetwork.Domain.Interfaces.Networks.Circuits.Forward;
using MyNeuralNetwork.Domain.Interfaces.Neurons;
using MyNeuralNetwork.Domain.Interfaces.Neurons.Activations;
using System;

namespace MyNeuralNetwork.Domain.Entities.Nets.Generators
{
    public class NNGenerator
    {
        private readonly INeuronGenerator _neuronGenerator;
        private readonly ILayersLinker _layersLinker;

        public ICircuitForward CircuitForward { get; set; } = new FeedForward();

        public NNGenerator(INeuronGenerator neuronGenerator, ILayersLinker layersLinker)
        {
            _neuronGenerator = neuronGenerator;
            _layersLinker = layersLinker;
        }

        public NeuralNetwork Generate<ISynapseManagerImplementation>(int[] formatLayers, IActivator[] activators)
        {
            if (formatLayers.Length != activators.Length) throw new ArgumentException("Size of parameters must be the same.");

            return new(_layersLinker.Generate(formatLayers.Length, (i, l) => CreateLayer<ISynapseManagerImplementation>(formatLayers[i], l, activators[i])), CircuitForward);
        }

        public NeuralNetwork Generate<ISynapseManagerImplementation, IActivatorImplementation>(int[] formatLayers)
        {
            return new(_layersLinker.Generate(formatLayers.Length, (i, l) => CreateLayer<ISynapseManagerImplementation, IActivatorImplementation>(formatLayers[i], l)), CircuitForward);
        }

        public NeuralNetwork GenerateDefault(int[] formatLayers)
        {
            return Generate<SynapseManager, Tanh>(formatLayers);
        }

        private Layer CreateLayer<T>(int quantityOfNeurons, LayerCounter layerCounter, IActivator activator)
        {
            return new(layerCounter, _neuronGenerator.Generate<T>(quantityOfNeurons, activator));
        }

        private Layer CreateLayer<T, U>(int quantityOfNeurons, LayerCounter layerCounter)
        {
            return new(layerCounter, _neuronGenerator.Generate<T, U>(quantityOfNeurons));
        }
    }
}