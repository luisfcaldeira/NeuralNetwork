using MyNeuralNetwork.Domain.Entities.Nets.Collections.Layers;
using MyNeuralNetwork.Domain.Entities.Nets.Layers;
using System;

namespace MyNeuralNetwork.Domain.Interfaces.Generators
{
    public interface ILayersLinker
    {
        LayerCollection Generate(int layers, Func<int, LayerCounter, Layer> createLayersMethod);
    }
}
