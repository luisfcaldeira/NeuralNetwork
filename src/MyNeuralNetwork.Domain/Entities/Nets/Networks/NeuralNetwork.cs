using MyNeuralNetwork.Domain.Entities.Nets.Collections.Layers;
using MyNeuralNetwork.Domain.Entities.Nets.Interfaces.Networks;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Outputs;
using MyNeuralNetwork.Domain.Entities.Nets.Layers;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyNeuralNetwork.Domain.Entities.Nets.Networks
{
    public class NeuralNetwork : INeuralNetwork
    {
        public LayerCollection Layers { get; private set; }

        public NeuralNetwork(LayerCollection layers)
        {
            Layers = layers;
        }

        public IEnumerable<Layer> GetNextLayer()
        {
            for(int i  = 0; i < Layers.Count; i++)
            {
                yield return Layers[i];
            }
        }

        public float[] Predict(Input[] inputs)
        {
            Layers.Predict(inputs);
            return Layers.Last().Output.ToArray();
        }

        public void Fit(Input[] inputs, Expected[] expectedCollection)
        {
            Layers.FeedForward(inputs);
            Layers.BackPropagate(expectedCollection);
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
