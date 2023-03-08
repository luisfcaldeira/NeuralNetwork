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
        public float Value { get; private set; } = 0;

        public Guid Guid { get; }
        public float Bias { get; private set; }
        public IActivator Activation { get; }
        public float Gamma { get; private set; }
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

        public void Feed(FloatNeuralValue input)
        {
            Value = input.Value;
        }

        public void Predict(Input input)
        {
            Value = input.Value;
        }

        internal Output GetOutput(Neuron neightborNeuron)
        {
            float _weight = GetWeight(neightborNeuron);
            return new Output(Value * _weight);
        }

        public void FeedForward(Input input)
        {
            Value = Activation.Activate(input.Value + Bias);
        }

        public void UpdateGamma(Feedback feedback)
        {
            Gamma = feedback.Value * Activation.Derivative(Value);
        }

        internal void UpdateValuesAndWeights(Layer previousLayer, Neuron neightborNeuron)
        {
            if(previousLayer != null)
            {
                previousLayer.Neurons.ForEach(n =>
                {
                    Bias -= n.Gamma * LearningRate;
                    Synapses.GetSynapse(neightborNeuron).Weight -= n.Gamma * Value * LearningRate;
                });
            }
        }

        internal void SumGama(Layer nextLayer, Neuron neightborNeuron)
        {
            if (nextLayer != null)
            {
                nextLayer.Neurons.ForEach(nextNeuron =>
                {
                    Gamma += GetWeight(neightborNeuron) * nextNeuron.Gamma;
                });
            }
        }

        internal void CommitGamma()
        {
            Gamma = Gamma * Activation.Derivative(Value);
        }

        internal void UpdateHiddenBackPropagation(Layer actualLayer, Neuron neightborNeuron)
        {
            var nextLayerNeurons = actualLayer.NextLayer.Neurons;

            Bias -= nextLayerNeurons.SumGammaDotFloat(LearningRate);
            Synapses.GetSynapse(neightborNeuron).Weight -= nextLayerNeurons.SumGammaDotFloat(LearningRate * Value);
        }

        private float GetWeight(Neuron neightborNeuron)
        {
            return Synapses.GetSynapse(neightborNeuron).Weight;
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
