using MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts;
using MyNeuralNetwork.Domain.Entities.Support;
using MyNeuralNetwork.Domain.Interfaces.Neurons.Activations;
using MyNeuralNetwork.Domain.Interfaces.Neurons.Parts;
using System;
using System.Linq;
using System.Text;

namespace MyNeuralNetwork.Domain.Entities.Nets.Neurons
{
    public class Neuron
    {
        public const double DeafultLearningRate = 0.01f;
        public NeuralDoubleValue Value { get; set; } = new NeuralDoubleValue();
        private double _tempValue = 0;
        protected static int index = 0;

        public int Index { get; private set; }
        public double Bias { get; set; }
        public IActivator Activation { get; }
        public double Gamma { get; set; }
        public ISynapseManager Synapses { get; }
        public double LearningRate { get; set; } = DeafultLearningRate;

        public Neuron(IActivator activation, NeuralDoubleValue bias, ISynapseManager synapseManager)
        {
            if (bias is null)
            {
                throw new ArgumentNullException(nameof(bias));
            }

            Index = index++;
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

        internal void Transmit(Transmition transmition)
        {
            _tempValue += transmition.Value;
        }

        internal void Commit()
        {
            _tempValue = Activation.Activate(_tempValue + Bias);
            Value.Set(_tempValue);
            _tempValue = 0;
        }

        internal void UpdateGamma(NeuralDoubleValue gamma)
        {
            Gamma = (Activation.Derivative(Value) * gamma).Value;
        }

        internal double GetOutput(Neuron neightborNeuron)
        {
            double _weight = GetWeight(neightborNeuron);
            return Value.Value * _weight;
        }

        private double GetWeight(Neuron neightborNeuron)
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
                   Index.Equals(neuron.Index);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Index);
        }

        internal void ChangeIndex(int i)
        {
            Index = i;
        }

        internal static void ResetIndex(int i)
        {
            index = i;
        }

        internal void Mutate(double chanceOfMutate, Neuron neuron)
        {
            if(MyRandom.Range(0, 1) < chanceOfMutate)
            {
                Bias = neuron.Bias;
                Synapses.Mutate(neuron.Synapses);
            }
        }
    }
}
