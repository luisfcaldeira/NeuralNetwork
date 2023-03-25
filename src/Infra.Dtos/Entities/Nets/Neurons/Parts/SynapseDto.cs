namespace Infra.Dtos.Entities.Nets.Neurons.Parts
{
    public class SynapseDto
    {
        public double Weight { get; set; }
        public NeuronDto NeuronSource { get; set; }
        public NeuronDto NeighborNeuron { get; set; }
    }
}
