using MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics;

namespace MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts
{
    public class Synapse
    {

        public NeuralFloatValue Weight { get; internal set; }
        public Neuron NeuronSource { get; private set; }
        public Neuron NeighborNeuron { get; private set; }

        public Synapse(RandomFloatValue weight, Neuron neuronSource, Neuron neighborNeuron)
        {
            Weight = weight;
            NeighborNeuron = neighborNeuron;
            NeuronSource = neuronSource;
        }

        public NeuralFloatValue GetOutput()
        {
            return NeuronSource.Value * Weight;
        }
    }
}
