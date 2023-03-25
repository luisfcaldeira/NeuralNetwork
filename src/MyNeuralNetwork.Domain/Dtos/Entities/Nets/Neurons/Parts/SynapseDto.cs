namespace MyNeuralNetwork.Domain.Dtos.Entities.Nets.Neurons.Parts
{
    public class SynapseDto
    {
        public double Weight { get; set; }
        public NeuronDto NeighborNeuron { get; set; }
    }
}
