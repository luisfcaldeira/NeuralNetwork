using MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics;
using MyNeuralNetwork.Domain.Interfaces.Neurons.Parts;
using System.Collections.Generic;
using System.Linq;

namespace MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts
{
    public class SynapseManager : ISynapseManager
    {
        public RangeConfiguration WeightConfiguration { get; set; } = new RangeConfiguration();
        public List<Synapse> Synapses { get; set; } = new List<Synapse>();

        public void Add(Neuron neuronSource, Neuron neighborNeuron)
        {
            RandomDoubleValue weight = new RandomDoubleValue(WeightConfiguration.MinimumRange, WeightConfiguration.MaximumRange);
            Synapses.Add(new Synapse(weight, neuronSource, neighborNeuron));
        }

        public int Count()
        {
            return Synapses.Count;
        }

        public double GetWeight(Neuron neighborNeuron)
        {
            return GetSynapse(neighborNeuron)
                 .Weight.Value;
        }

        public List<double> GetWeights(Neuron neighborNeuron)
        {
           return Synapses.Where(x => x.NeighborNeuron.Equals(neighborNeuron)).Select(x => x.Weight.Value).ToList();
        }

        public NeuralDoubleValue GetOutput(Neuron neighborNeuron)
        {
            var synapse = GetSynapse(neighborNeuron);
            return synapse.GetOutput();
        }

        public Synapse GetSynapse(Neuron neighborNeuron)
        {
            return Synapses.Where(n => n.NeighborNeuron.Equals(neighborNeuron))
                             .First();
        }

        public NeuralDoubleValue GetWeightFor(Neuron neighborNeuron)
        {
            return GetSynapse(neighborNeuron).Weight;
        }

        // TODO Tá errado isso aqui. Ele tem que somar os neurônios de origem, mas ele tá vinculado a uma origem só
        public void TransmitTo(Neuron target)
        {
            double output = GetSynapse(target).GetOutput().Value;

            target.Transmit(new Transmition(output));
        }
    }
}
