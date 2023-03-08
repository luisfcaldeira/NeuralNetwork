using MyNeuralNetwork.Domain.Entities.Nets.IO.Outputs;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts;
using System.Collections.Generic;

namespace MyNeuralNetwork.Domain.Entities.Nets.Interfaces.Neurons.Parts
{
    public interface ISynapseManager
    {
        RangeConfiguration WeightConfiguration { get; set; }
        List<Synapse> Synapses { get; }
        void Add(Neuron neuronSource, Neuron neighborNeuron);
        int Count();
        Output GetOutput(Neuron neighborNeuron);
        Synapse GetSynapse(Neuron neighborNeuron);
    }
}
