using MyNeuralNetwork.Domain.Entities.Nets.Collections.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.Collections.IO.Outputs;
using MyNeuralNetwork.Domain.Entities.Nets.Collections.Neurons;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Outputs;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons;
using System;
using System.Linq;
using System.Text;

namespace MyNeuralNetwork.Domain.Entities.Nets
{
    public class Layer
    {
        public NeuronCollection Neurons { get; set; }
        public Layer PreviousLayer { get; set; }
        public Layer NextLayer { get; set; }
        
        public Layer(NeuronCollection neurons)
        {
            Neurons = neurons;
        }

        public void Feedforward(InputCollection inputs)
        {
            Neurons.Feed(inputs);
            NextLayer?.HideFeedforward(Neurons.SumOutput());
        }

        private void HideFeedforward(float output)
        {
            Neurons.ForEach(neuron =>
            {
                neuron.FeedForward(new Input(output));
            });

            NextLayer?.HideFeedforward(Neurons.SumOutput());
        }

        public void BackPropagation(ExpectedCollection expecteds)
        {
            if (Neurons.Count != expecteds.Count)
                throw new ArgumentException($"Number of {nameof(expecteds)} must be the same as number of {nameof(Neurons)}.");

            for(int i = 0; i < Neurons.Count; i++)
            {
                Expected expected = expecteds[i];
                Feedback feedback = new Feedback(Neurons[i].GetOutput(), expected);

                Neurons[i].BackPropagation(feedback);
            }

            PreviousLayer?.HideBackPropagation(Neurons);
        }

        internal void HideBackPropagation(NeuronCollection neurons)
        {
            Neurons.ForEach(neuron =>
            {
                neuron.HideBackPropagation(neurons);
            });

            PreviousLayer?.HideBackPropagation(Neurons);
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
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
            Neurons.Predict(inputs);
        }
    }
}
