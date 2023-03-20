using MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics;
using MyNeuralNetwork.Domain.Entities.Nets.Interfaces.Neurons.Parts;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Outputs;
using System.Collections.Generic;
using System.Linq;

namespace MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts
{
    public class SynapseManager : ISynapseManager
    {
        public RangeConfiguration WeightConfiguration { get; set; } = new RangeConfiguration();
        public List<Synapse> Synapses { get; } = new List<Synapse>();

        public void Add(Neuron neuronSource, Neuron neighborNeuron)
        {
            RandomFloatValue weight = new RandomFloatValue(WeightConfiguration.MinimumRange, WeightConfiguration.MaximumRange);
            Synapses.Add(new Synapse(weight, neuronSource, neighborNeuron));
        }

        public int Count()
        {
            return Synapses.Count;
        }

        public float GetWeight(Neuron neighborNeuron)
        {
            return GetSynapse(neighborNeuron)
                 .Weight.Value;
        }

        public List<float> GetWeights(Neuron neighborNeuron)
        {
           return Synapses.Where(x => x.NeighborNeuron.Equals(neighborNeuron)).Select(x => x.Weight.Value).ToList();
        }

        public NeuralFloatValue GetOutput(Neuron neighborNeuron)
        {
            var synapse = GetSynapse(neighborNeuron);
            return synapse.GetOutput();
        }

        public Synapse GetSynapse(Neuron neighborNeuron)
        {
            return Synapses.Where(n => n.NeighborNeuron.Equals(neighborNeuron))
                             .First();
        }

        public NeuralFloatValue GetWeightFor(Neuron neighborNeuron)
        {
            return GetSynapse(neighborNeuron).Weight;
        }

        public void TransmitTo(Neuron target)
        {
            var sumOfOutputs = Synapses.Where(s => s.NeighborNeuron.Equals(target))
                .Sum(s =>
                {
                    return s.GetOutput().Value;
                });

            target.Feed(new Transmition(sumOfOutputs));
        }
    }
}
