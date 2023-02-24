using MyNeuralNetwork.Domain.Entities.Nets.Collections.Neurons;
using MyNeuralNetwork.Domain.Entities.Nets.Interfaces.Neurons.Activations;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Outputs;

namespace MyNeuralNetwork.Domain.Entities.Nets.Neurons
{
    public class Neuron
    {
        public float Value { get; private set; } = 0;
        public float Gamma { get; private set; } = 0;

        public Bias Bias { get; set; }
        public Weight Weight { get; set; }
        public IActivation Activation { get; }
        public float LearningRate { get; set; } = 0.01f;
        public Expected Expected { get; set; }

        public Neuron(IActivation activation)
        {
            Bias = new Bias(LearningRate);
            Weight = new Weight(LearningRate);

            Activation = activation;
        }

        public void Fit(Input input)
        {
            Value = input.Value;
        }

        public void Predict(Input input)
        {

        }

        internal Output GetOutput()
        {
            return new Output(Value * Weight.Value);
        }

        public void FeedForward(Input input)
        {
            Value = Activation.Activate(input.Value + Bias.Value);
        }

        public void BackPropagation(Feedback feedback)
        {
            Gamma = feedback.Value * Activation.Derivative(Value);
        }

        internal void HideBackPropagation(NeuronCollection neurons)
        {
            var newGamma = neurons.SumGammaDotFloat(LearningRate);
            Bias.Fix(newGamma);

            newGamma = neurons.SumGammaDotFloat(Weight.Value) * Activation.Derivative(Value);

            Gamma = newGamma;
            
            Weight.Fix(neurons.SumGammaDotFloat(Value * LearningRate));
        }

        public override string ToString()
        {
            return Value.ToString();
        }

    }
}
