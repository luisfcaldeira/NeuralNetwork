using MyNeuralNetwork.Domain.Dtos.Entities.Nets.Layers;
using System.Collections.Generic;

namespace MyNeuralNetwork.Domain.Dtos.Entities.Nets.Networks
{
    public class NeuralNetworkDto
    {
        public List<LayerDto> Layers { get; set; }
        public string CircuitForward { get; set; }
    }
}
