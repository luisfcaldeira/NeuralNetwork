using MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Outputs;
using MyNeuralNetwork.Domain.Entities.Nets.Layers;

namespace MyNeuralNetwork.Domain.Entities.Nets.Networks.Circuits.Backward.Support
{
    public class LastLayerFiller
    {

        public static void UpdateLayerIfItsLastOne(Expected[] expecteds, Layer layer)
        {
            if (layer.IsLast())
            {
                UpdateOutputLayer(expecteds, layer);
            }
        }

        private static void UpdateOutputLayer(Expected[] expecteds, Layer layer)
        {
            for (var i = 0; i < layer.Neurons.Count; i++)
            {
                layer.Neurons[i].UpdateGamma(CalculateGamma(layer.Neurons[i].Value, expecteds[i]));
            }
        }

        private static NeuralDoubleValue CalculateGamma(NeuralDoubleValue neuronValue, NeuralDoubleValue expected)
        {
            return new(neuronValue.Value - expected.Value);
        }
    }
}
