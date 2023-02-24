using MyNeuralNetwork.Domain.Entities.Nets.Collections.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.Collections.IO.Outputs;
using MyNeuralNetwork.Domain.Entities.Nets.Collections.Layers;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Outputs;
using System;
using System.Collections.Generic;

namespace MyNeuralNetwork.Domain.Entities.Nets
{
    public class NeuralNetwork : IComparable<NeuralNetwork>
    {
        private LayerCollection Layers { get; set; }
        public float fitness = 0;

        public NeuralNetwork(LayerCollection layers)
        {
            Layers = layers;
        }

        public void Predict(InputCollection inputs)
        {
            Layers.Predict(inputs);
        }

        public void Fit(InputCollection inputs)
        {
            Layers.FeedForward(inputs);
        }

        public void Backpropagation(ExpectedCollection expectedCollection)
        {
            Layers.BackPropagation(expectedCollection);
        }

        public void PrintLayers()
        {
            foreach (var layer in Layers)
            {
                Console.WriteLine(layer);
            }
        }

        public int CompareTo(NeuralNetwork other) //Comparing For NeuralNetworks performance.
        {
            if (other == null) return 1;

            if (fitness > other.fitness)
                return 1;
            else if (fitness < other.fitness)
                return -1;
            else
                return 0;
        }

    }

}
