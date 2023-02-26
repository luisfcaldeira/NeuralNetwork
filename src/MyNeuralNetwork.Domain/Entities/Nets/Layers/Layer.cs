using MyNeuralNetwork.Domain.Entities.Nets.Collections.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.Collections.IO.Outputs;
using MyNeuralNetwork.Domain.Entities.Nets.Collections.Neurons;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Outputs;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons;
using System;
using System.Text;

namespace MyNeuralNetwork.Domain.Entities.Nets.Layers
{
    public class Layer
    {
        public NeuronCollection Neurons { get; set; }
        public Layer PreviousLayer { get; set; }
        public Layer NextLayer { get; set; }
        public float Output { get; private set; }

        private static int _labelCounter = 0;
        public int Label { get; }

        public Layer(NeuronCollection neurons)
        {
            Neurons = neurons;
            Label = _labelCounter++;
        }

        public void Feedforward(InputCollection inputs)
        {
            Neurons.Feed(inputs);
            Output = Neurons.SumOutput();
            NextLayer?.FeedFoward(Output);
        }

        private void FeedFoward(float output)
        {
            Neurons.ForEach(neuron =>
            {
                neuron.FeedForward(new Input(output));
            });

            Output = Neurons.SumOutput();
            NextLayer?.FeedFoward(Output);
        }

        public void BackPropagate(ExpectedCollection expecteds)
        {
            if (Neurons.Count != expecteds.Count)
                throw new ArgumentException($"Number of {nameof(expecteds)} must be the same as number of {nameof(Neurons)}.");

            UpdateOutputGamma(expecteds);

            UpdatePreviousLayers();
        }

        internal void UpdateOutputGamma(ExpectedCollection expecteds)
        {
            for (int i = 0; i < Neurons.Count; i++)
            {
                Expected expected = expecteds[i];
                Feedback feedback = new Feedback(Neurons[i].GetOutput(), expected);

                Neurons[i].UpdateGamma(feedback);
            }
        }

        private void UpdatePreviousLayers()
        {
            if(PreviousLayer  != null)
            {
                foreach (var neuron in PreviousLayer.Neurons)
                {
                    neuron.SumGama(this);
                    neuron.UpdateValuesAndWeights(this);
                }

                PreviousLayer.UpdateNeurons();
            }
        }

        internal void UpdateNeurons()
        {
            Neurons.ForEach(neuron =>
            {
                neuron.CommitGamma();
                neuron.UpdateHiddenBackPropagation(this);
            });

            PreviousLayer?.UpdateNeurons();
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{Label}: ");
            var comma = "";

            foreach (Neuron neuron in Neurons)
            {
                stringBuilder.Append(comma);
                stringBuilder.Append(neuron.ToString());
                comma = ", ";
            }

            return stringBuilder.ToString();
        }

        internal void Predict(InputCollection inputs)
        {
            Feedforward(inputs);
        }
    }
}
