using MyNeuralNetwork.Domain.Entities.Nets.Interfaces.Neurons.Activations;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Outputs;
using MyNeuralNetwork.Domain.Entities.Nets.Layers;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts;

namespace MyNeuralNetwork.Domain.Entities.Nets.Neurons
{
    public class Neuron
    {
        public float Value { get; private set; } = 0;

        public Bias Bias { get; set; }
        public Weight Weight { get; set; }
        public IActivation Activation { get; }
        public Gamma Gamma { get; private set; }

        public float LearningRate { get; set; } = 0.01f;
        public Expected Expected { get; set; }

        public Neuron(IActivation activation)
        {
            Bias = new Bias();
            Weight = new Weight();
            Activation = activation;
            Gamma = new Gamma(activation);
        }

        public void Fit(Input input)
        {
            Value = input.Value;
        }

        public void Predict(Input input)
        {
            Value = input.Value;
        }

        internal Output GetOutput()
        {
            return new Output(Value * Weight.Value);
        }

        public void FeedForward(Input input)
        {
            Value = Activation.Activate(input.Value + Bias.Value);
        }

        internal void UpdateGamma(Feedback feedback)
        {
            Gamma.SetValue(feedback, this);
        }

        internal void UpdateValuesAndWeights(Layer previousLayer)
        {
            if(previousLayer != null)
            {
                previousLayer.Neurons.ForEach(n =>
                {
                    Bias.Value -= n.Gamma * LearningRate;
                    Weight.Value -= n.Gamma * Value * LearningRate;
                });
            }
        }

        internal void SumGama(Layer nextLayer)
        {
            if (nextLayer != null)
            {
                nextLayer.Neurons.ForEach(nextNeuron =>
                {
                    Gamma.Value += Weight * nextNeuron.Gamma;
                });
            }
        }

        internal void CommitGamma()
        {
            Gamma.Value = Gamma.Value * Activation.Derivative(Value);
        }

        internal void UpdateHiddenBackPropagation(Layer actualLayer)
        {
            var nextLayerNeurons = actualLayer.NextLayer.Neurons;

            Bias.Fix(nextLayerNeurons.SumGammaDotFloat(LearningRate));
            Weight.Fix(nextLayerNeurons.SumGammaDotFloat(LearningRate * Value));
        }

        public override string ToString()
        {

            return $"\n[v:{Value}] \tw{Weight} - b{Bias} - g{Gamma}";
        }

    }
}
