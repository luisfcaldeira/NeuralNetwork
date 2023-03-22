using MyNeuralNetwork.Domain.Entities.Nets.Collections.Layers;
using MyNeuralNetwork.Domain.Entities.Nets.Interfaces.Networks;
using MyNeuralNetwork.Domain.Entities.Nets.Interfaces.Networks.Circuits.Backward;
using MyNeuralNetwork.Domain.Entities.Nets.Interfaces.Networks.Circuits.Forward;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.Layers;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyNeuralNetwork.Domain.Entities.Nets.Networks
{
    public class NeuralNetwork : INeuralNetwork
    {
        public LayerCollection Layers { get; private set; }
        private readonly ICircuitForward _circuitForward;

        public NeuralNetwork(LayerCollection layers, ICircuitForward circuitForward)
        {
            Layers = layers;
            _circuitForward = circuitForward;
        }

        public IEnumerable<Layer> GetNextLayer()
        {
            for(int i  = 0; i < Layers.Count; i++)
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
            _circuitForward.Send(this, inputs);

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

    }

}
