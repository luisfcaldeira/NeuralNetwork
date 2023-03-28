using MyNeuralNetwork.Domain.Dtos.Entities.Nets.Neurons.Parts;

namespace MyNeuralNetwork.Domain.Dtos.Entities.Nets.Neurons
{
    public class NeuronDto
    {
        public string Activator { get; set; }
        public int Guid { get; set; }
        public double Bias { get; set; }
        public SynapseManagerDto Synapses { get; set; }
        public double LearningRate { get; set; }
    }
}
