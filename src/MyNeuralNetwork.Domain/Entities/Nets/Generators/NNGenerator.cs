using MyNeuralNetwork.Domain.Entities.Nets.Collections.Layers;
using MyNeuralNetwork.Domain.Entities.Nets.Interfaces.Networks.Circuits.Forward;
using MyNeuralNetwork.Domain.Entities.Nets.Interfaces.Neurons;
using MyNeuralNetwork.Domain.Entities.Nets.Layers;
using MyNeuralNetwork.Domain.Entities.Nets.Networks;
using MyNeuralNetwork.Domain.Entities.Nets.Networks.Circuits.Forward;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Activations;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts;

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

        public NeuralNetwork Generate<ISynapseManagerImplementation, IActivatorImplementation>(int[] formatLayers)
        {
            var layers = new LayerCollection();
            Layer previousLayer = null;
            LayerCounter layerCounter = new LayerCounter();

            for (int i = 0; i < formatLayers.Length; i++)
            {
                Layer layer = CreateLayer<ISynapseManagerImplementation, IActivatorImplementation>(formatLayers, layerCounter, i);

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
            }

            return new NeuralNetwork(layers, CircuitForward);
        }

        public NeuralNetwork GenerateDefault(int[] formatLayers)
        {
            return Generate<SynapseManager,Tanh>(formatLayers);
        }

        private Layer CreateLayer<T, U>(int[] formatLayers, LayerCounter layerCounter, int i)
        {
            return new(layerCounter, _neuronGenerator.Generate<T, U>(formatLayers[i]));
        }
    }
}