using MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics;

namespace MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts
{
    public class Synapse
    {
        private Neuron _neuronSource;

        public float Weight { get; internal set; }
        public Neuron NeighborNeuron { get; private set; }

        public Synapse(RandomFloatValue weight, Neuron neuronSource, Neuron neighborNeuron)
        {
            Weight = weight.Value;
            NeighborNeuron = neighborNeuron;
            _neuronSource = neuronSource;
        }

        public float GetOutput()
        {
            return _neuronSource.Value * Weight;
        }
    }
}
