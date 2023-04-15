using MyNeuralNetwork.Domain.Dtos.Entities.Nets.Neurons;
using System.Collections.Generic;

namespace MyNeuralNetwork.Domain.Dtos.Entities.Nets.Layers
{
    public class LayerDto
    {
        public List<NeuronDto> Neurons { get; set; }
        public int Label { get; set; }
    }
}
