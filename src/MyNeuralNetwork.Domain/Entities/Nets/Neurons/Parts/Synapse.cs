using MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics;

namespace MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts
{
    public class Synapse
    {

        public NeuralDoubleValue Weight { get; internal set; }
        public Neuron NeuronSource { get; private set; }
        public Neuron NeighborNeuron { get; private set; }

        public Synapse(RandomDoubleValue weight, Neuron neuronSource, Neuron neighborNeuron)
        {
            Weight = new NeuralDoubleValue(weight.Value);
            NeighborNeuron = neighborNeuron;
            NeuronSource = neuronSource;
        }

        public NeuralDoubleValue GetOutput()
        {
            return new(NeuronSource.Value.Value * Weight.Value);
        }
    }
}
