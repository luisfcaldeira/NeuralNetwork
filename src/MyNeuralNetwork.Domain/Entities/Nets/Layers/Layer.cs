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

        internal bool IsLast()
        {
            return NextLayer == null;
        }

        public void Add(Input[] inputs)
        {
            Neurons.Feed(inputs);
        }

        public void UpdateOutput()
        {
            Output = new List<float>();
            foreach (var neuron in Neurons)
            {
                Output.Add(neuron.Value.Value);
            }
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

    }
}
