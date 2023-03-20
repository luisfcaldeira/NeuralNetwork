using MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics;
using MyNeuralNetwork.Domain.Entities.Nets.Interfaces.Neurons.Activations;
using MyNeuralNetwork.Domain.Entities.Nets.Interfaces.Neurons.Parts;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Outputs;
using MyNeuralNetwork.Domain.Entities.Nets.Layers;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts;
using System;
using System.Linq;
using System.Text;

namespace MyNeuralNetwork.Domain.Entities.Nets.Neurons
{
    public class Neuron
    {
        public NeuralFloatValue Value { get; private set; } = new NeuralFloatValue();

        public Guid Guid { get; }
        public float Bias { get; set; }
        public IActivator Activation { get; }
        public float Gamma { get; set; }
        public ISynapseManager Synapses { get; }

        public float LearningRate { get; set; } = 0.01f;
        
        public Neuron(IActivator activation, RandomFloatValue bias, ISynapseManager synapseManager)
        {
            if (bias is null)
            {
                throw new ArgumentNullException(nameof(bias));
            }

            Guid = Guid.NewGuid();
            Bias = bias.Value;
            Synapses = synapseManager ?? throw new ArgumentNullException(nameof(synapseManager));
            Activation = activation ?? throw new ArgumentNullException(nameof(activation));
        }

        internal void CreateSynapse(Neuron layer1Neuron)
        {
            Synapses.Add(this, layer1Neuron);
        }

        public void Feed(Input input)
        {
            Value.Set(input.Value);
        }

        internal void Feed(Transmition transmition)
        {
            Value.Set(Activation.Activate(transmition.Value));
        }

        internal void UpdateGamma(NeuralFloatValue gamma)
        {
            Gamma = Activation.Derivative(Value.Value) * gamma.Value;
        }

        internal NeuralFloatValue GetOutput(Neuron neightborNeuron)
        {
            float _weight = GetWeight(neightborNeuron);
            return Value * _weight;
        }

        private float GetWeight(Neuron neightborNeuron)
        {
            return Synapses.GetSynapse(neightborNeuron).Weight.Value;
        }

        public override string ToString()
        {
            var weights = Synapses.Synapses.Select(x => x.Weight);
            var stringBuilder = new StringBuilder();
            var semicolon = "";
            var count = 1;
            foreach(var weight in weights)
            {
                stringBuilder.Append(semicolon);
                stringBuilder.Append($"w{count++}: ");
                stringBuilder.Append(weight);
                semicolon = "; ";
            }

            return $"N:(v:{Value}];{stringBuilder};b:{Bias};g:{Gamma})";
        }

        public override bool Equals(object obj)
        {
            return obj is Neuron neuron &&
                   Guid.Equals(neuron.Guid);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Guid);
        }
    }
}
