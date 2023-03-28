using MyNeuralNetwork.Domain.Entities.Nets.Collections.Layers;
using MyNeuralNetwork.Domain.Entities.Nets.Layers;
using MyNeuralNetwork.Domain.Entities.Nets.Networks.Circuits.Forward;
using MyNeuralNetwork.Domain.Interfaces.Generators;
using MyNeuralNetwork.Domain.Interfaces.Networks.Circuits.Forward;
using System;

namespace MyNeuralNetwork.Domain.Entities.Nets.Generators.Supports
{
    public class LayersLinker : ILayersLinker
    {
        public ICircuitForward CircuitForward { get; set; } = new FeedForward();

        public LayerCollection Generate(int layers, Func<int, LayerCounter, Layer> createLayersMethod)
        {
            Layer previousLayer = null;
            LayerCollection layersCollection = new();
            LayerCounter layerCounter = new();

            for (int i = 0; i < layers; i++)
            {
                Layer layer = createLayersMethod.Invoke(i, layerCounter);

                layer.PreviousLayer = previousLayer;

                if (i > 0)
                {
                    layersCollection[i - 1].NextLayer = layer;
                }

                previousLayer = layer;

                layersCollection.Add(layer);
            }

            return layersCollection;
        }

    }
}
