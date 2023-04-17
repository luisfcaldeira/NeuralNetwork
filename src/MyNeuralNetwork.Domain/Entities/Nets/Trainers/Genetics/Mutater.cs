using MyNeuralNetwork.Domain.Entities.Nets.Layers;
using MyNeuralNetwork.Domain.Entities.Support;
using MyNeuralNetwork.Domain.Interfaces.Networks;

namespace MyNeuralNetwork.Domain.Entities.Nets.Trainers.Genetics
{
    public class Mutater
    {
        public double ChanceOfMutate { get; set; } = 0.9;
        public double Min { get; set; } = -0.01;
        public double Max { get; set; } = 0.01;

        public Mutater()
        {
        }

        public Mutater(double chanceOfMutate, double min, double max)
        {
            ChanceOfMutate = chanceOfMutate;
            Min = min;
            Max = max;
        }

        public void Mutate(INeuralNetwork targetNetwork, INeuralNetwork sourceNetwork)
        {
            if (!PassChance())
            {
                return;
            }

            targetNetwork.CounterOfMutations++;

            for (var i = 0; i < targetNetwork.Layers.Count; i++)
            {
                var layer = targetNetwork.Layers[i];
                var parentLayer = sourceNetwork.Layers[i];

                MutateNeurons(layer, parentLayer);
            }
        }

        private void MutateNeurons(Layer layer, Layer parentLayer)
        {
            for (var i = 0; i < layer.Neurons.Count; i++)
            {
                layer.Neurons[i].Mutate(parentLayer.Neurons[i], Min, Max);
            }
        }

        private bool PassChance()
        {
            return MyRandom.Range(0, 1) < ChanceOfMutate;
        }
    }
}
