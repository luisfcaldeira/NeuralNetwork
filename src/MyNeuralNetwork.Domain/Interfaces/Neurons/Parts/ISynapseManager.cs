using MyNeuralNetwork.Domain.Dtos.Entities.Nets.Neurons;
using MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts;
using System.Collections.Generic;

namespace MyNeuralNetwork.Domain.Interfaces.Neurons.Parts
{
    public interface ISynapseManager
    {
        RangeConfiguration WeightConfiguration { get; set; }
        List<Synapse> Synapses { get; }
        
        void Add(Neuron neuronSource, Neuron neighborNeuron);
        int Count();
        NeuralDoubleValue GetOutput(Neuron neighborNeuron);
        Synapse GetSynapse(Neuron neighborNeuron);
        NeuralDoubleValue GetWeightFor(Neuron neighborNeuron);
        void TransmitTo(Neuron target);
        void ChangeWeights(List<NeuronDto> neuronsDto);
        void Mutate(ISynapseManager synapses, double min = -0.01, double max = 0.01);
    }
}
