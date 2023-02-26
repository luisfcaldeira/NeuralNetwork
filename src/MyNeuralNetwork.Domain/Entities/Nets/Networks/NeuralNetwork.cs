using MyNeuralNetwork.Domain.Entities.Nets.Collections.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.Collections.IO.Outputs;
using MyNeuralNetwork.Domain.Entities.Nets.Collections.Layers;
using System;
using System.Linq;

namespace MyNeuralNetwork.Domain.Entities.Nets.Networks
{
    public class NeuralNetwork : IComparable<NeuralNetwork>
    {
        private LayerCollection Layers { get; set; }
        public float fitness = 0;

        public NeuralNetwork(LayerCollection layers)
        {
            Layers = layers;
        }

        public float Predict(InputCollection inputs)
        {
            Layers.Predict(inputs);
            return Layers.Last().Output;
        }

        public void Fit(InputCollection inputs)
        {
            Layers.FeedForward(inputs);
        }

        public void Backpropagate(ExpectedCollection expectedCollection)
        {
            Layers.BackPropagate(expectedCollection);
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
