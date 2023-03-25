using MyNeuralNetwork.Domain.Interfaces.Neurons.Activations;
using MyNeuralNetwork.Domain.Interfaces.Neurons.Parts;
using System;

namespace Infra.Dtos.Entities.Nets.Neurons
{
    public class NeuronDto
    {
        public Guid Guid { get; }
        public double Value { get; set; }
        public double Bias { get; set; }
        public IActivator Activation { get; }
        public double Gamma { get; set; }
        public ISynapseManager Synapses { get; }
        public double LearningRate { get; set; }
    }
}
