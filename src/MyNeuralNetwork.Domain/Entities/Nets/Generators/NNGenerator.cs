using MyNeuralNetwork.Domain.Entities.Nets.Collections.Layers;
using MyNeuralNetwork.Domain.Entities.Nets.Layers;
using MyNeuralNetwork.Domain.Entities.Nets.Networks;
using MyNeuralNetwork.Domain.Entities.Nets.Networks.Circuits.Forward;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Activations;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts;
using MyNeuralNetwork.Domain.Interfaces.Networks.Circuits.Forward;
using MyNeuralNetwork.Domain.Interfaces.Neurons;
using MyNeuralNetwork.Domain.Interfaces.Neurons.Activations;
using System;

namespace MyNeuralNetwork.Domain.Entities.Nets.Generators
{
    public class NNGenerator
    {
        private readonly INeuronGenerator _neuronGenerator;
        public ICircuitForward CircuitForward { get; set; } = new FeedForward();

        public NNGenerator(INeuronGenerator neuronGenerator)
        {
            _neuronGenerator = neuronGenerator;
        }

        public NeuralNetwork Generate<ISynapseManagerImplementation>(int[] formatLayers, IActivator[] activators)
        {
            if (formatLayers.Length != activators.Length) throw new ArgumentException("Size of parameters must be the same.");

            return Generate((i, l) => CreateLayer<ISynapseManagerImplementation>(formatLayers[i], l, activators[i]), formatLayers);
        }


        public NeuralNetwork Generate<ISynapseManagerImplementation, IActivatorImplementation>(int[] formatLayers)
        {
            return Generate((i, l) => CreateLayer<ISynapseManagerImplementation, IActivatorImplementation>(formatLayers[i], l), formatLayers);
        }

        private NeuralNetwork Generate(Func<int, LayerCounter, Layer> createLayersMethod, int[] formatLayers)
        {
            var layers = new LayerCollection();
            Layer previousLayer = null;
            LayerCounter layerCounter = new LayerCounter();

            for (int i = 0; i < formatLayers.Length; i++)
            {
                Layer layer = createLayersMethod.Invoke(i, layerCounter);

                previousLayer = ConfigLayers(layers, previousLayer, i, layer);
            }

            return new NeuralNetwork(layers, CircuitForward);

        }

        private static Layer ConfigLayers(LayerCollection layers, Layer previousLayer, int i, Layer layer)
        {
            if (previousLayer != null)
            {
                layer.PreviousLayer = previousLayer;
            }

            if (i > 0)
            {
                layers[i - 1].NextLayer = layer;
            }

            layers.Add(layer);
            previousLayer = layer;
            return previousLayer;
        }

        public NeuralNetwork GenerateDefault(int[] formatLayers)
        {
            return Generate<SynapseManager,Tanh>(formatLayers);
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