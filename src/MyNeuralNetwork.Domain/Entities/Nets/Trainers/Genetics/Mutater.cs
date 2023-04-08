using MyNeuralNetwork.Domain.Entities.Nets.Layers;
using MyNeuralNetwork.Domain.Interfaces.Networks;

namespace MyNeuralNetwork.Domain.Entities.Nets.Trainers.Genetics
{
    internal class Mutater 
    {
        public static double ChanceOfMutate { get; set; } = 0.5;

        public static void Mutate(INeuralNetwork targetNetwork, INeuralNetwork sourceNetwork)
        {
            for(var i = 0; i < targetNetwork.Layers.Count; i++)
            {
                var layer = targetNetwork.Layers[i];
                var parentLayer = sourceNetwork.Layers[i];

                MutateNeurons(layer, parentLayer);
            }
        }

        private static void MutateNeurons(Layer layer, Layer parentLayer)
        {
            for (var i = 0; i < layer.Neurons.Count; i++)
            {
                layer.Neurons[i].Mutate(ChanceOfMutate, parentLayer.Neurons[i]);
            }
        }
    }
}
