using MyNeuralNetwork.Domain.Entities.Nets.Collections.Neurons;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Outputs;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyNeuralNetwork.Domain.Entities.Nets.Layers
{
    public class Layer
    {
        public NeuronCollection Neurons { get; set; } = new NeuronCollection();
        public Layer PreviousLayer { get; set; }
        public Layer _nextLayer;
        public Layer NextLayer 
        {
            get
            {
                return _nextLayer;
            }
            set
            {
                this.Neurons.ForEach(myNeuron =>
                {
                    value.Neurons.ForEach(theirNeuron =>
                    {
                        myNeuron.CreateSynapse(theirNeuron);
                    });
                });
                _nextLayer = value;
            }
        }

        public List<float> Output { get; private set; }

        public int Label { get; }

        public Layer(LayerCounter layerCounter, NeuronCollection neurons)
        {
            Output = new List<float>();
            Neurons = neurons;
            Label = layerCounter.Counter;
        }

        public void Send(Input[] inputs)
        {
            Neurons.Feed(inputs);
            UpdateOutput();
        }

        public void Feedforward(Input[] inputs)
        {
            Neurons.Feed(inputs);
            UpdateOutput();
            float output = Neurons.SumOutputDotWeight(NextLayer?.Neurons);
            NextLayer?.FeedFoward(output);
        }

        private void UpdateOutput()
        {
            Output = new List<float>();
            foreach (var neuron in Neurons)
            {
                Output.Add(neuron.Value);
            }
        }

        private void FeedFoward(float output)
        {
            Neurons.ForEach(neuron =>
            {
                neuron.FeedForward(new Input(output));
            });

            UpdateOutput();
            NextLayer?.FeedFoward(Neurons.SumOutputDotWeight(NextLayer.Neurons));
        }

        public void BackPropagate(Expected[] expecteds)
        {
            if (Neurons.Count != expecteds.Length)
                throw new ArgumentException($"Number of {nameof(expecteds)} must be the same as number of {nameof(Neurons)}.");

            UpdateOutputGamma(expecteds);

            UpdatePreviousLayers();
        }

        internal void UpdateOutputGamma(Expected[] expecteds)
        {
            for (int i = 0; i < Neurons.Count; i++)
            {
                Expected expected = expecteds[i];
                Neurons.ForEach(theirNeuron =>
                {
                    Feedback feedback = new Feedback(Neurons[i].Value, expected);
                    Neurons[i].UpdateGamma(feedback);
                });
            }
        }

        private void UpdatePreviousLayers()
        {
            if(PreviousLayer  != null)
            {
                foreach (var neuron in PreviousLayer.Neurons)
                {
                    Neurons.ForEach(myNeuron =>
                    {
                        neuron.SumGama(this, myNeuron);
                        neuron.UpdateValuesAndWeights(this, myNeuron);
                    });
                }

                PreviousLayer.UpdateNeurons();
            }
        }

        internal void UpdateNeurons()
        {
            Neurons.ForEach(myNeuron =>
            {
                NextLayer.Neurons.ForEach(theirNeuron =>
                {
                    myNeuron.CommitGamma();
                    myNeuron.UpdateHiddenBackPropagation(this, theirNeuron);
                });
            });

            PreviousLayer?.UpdateNeurons();
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            foreach (Neuron neuron in Neurons)
            {
                stringBuilder.Append($"L{Label}:");
                stringBuilder.AppendLine(neuron.ToString());
            }

            return stringBuilder.ToString();
        }

        internal void Predict(Input[] inputs)
        {
            Feedforward(inputs);
        }
    }
}
