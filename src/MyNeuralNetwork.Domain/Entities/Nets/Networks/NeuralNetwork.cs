using MyNeuralNetwork.Domain.Entities.Nets.Collections.Layers;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.Layers;
using MyNeuralNetwork.Domain.Interfaces.Networks;
using MyNeuralNetwork.Domain.Interfaces.Networks.Circuits.Forward;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyNeuralNetwork.Domain.Entities.Nets.Networks
{
    public class NeuralNetwork : INeuralNetwork
    {
        public double Fitness { get; set; }
        public LayerCollection Layers { get; protected set; }
        public int CounterOfMutations { get; set; } = 0;

        public ICircuitForward CircuitForward { get; }

        public NeuralNetwork()
        {
            Layers = new LayerCollection();
        }

        public NeuralNetwork(LayerCollection layers, ICircuitForward circuitForward)
        {
            Layers = layers;
            CircuitForward = circuitForward;
        }

        public IEnumerable<Layer> GetNextLayer()
        {
            for (int i = 0; i < Layers.Count; i++)
            {
                yield return Layers[i];
            }
        }

        public IEnumerable<Layer> GetBackLayers()
        {
            for (int i = Layers.Count - 1; i >= 0; i--)
            {
                yield return Layers[i];
            }
        }

        public double[] Predict(Input[] inputs)
        {
            CircuitForward.Send(this, inputs);

            return Layers.Last().Output.ToArray();
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            foreach (var layer in Layers)
            {
                stringBuilder.Append(layer.ToString());
            }

            return stringBuilder.ToString();
        }

        public int CompareTo(INeuralNetwork other)
        {
            if (other == null) return 1;

            if (Fitness > other.Fitness)
                return 1;

            if (Fitness < other.Fitness)
                return -1;

            return 0;
        }

    }
}
