using MyNeuralNetwork.Domain.Interfaces.Networks;
using System.Collections.Generic;
using System.Linq;

namespace MyNeuralNetwork.Domain.Entities.Nets.Trainers.Genetics
{
    public class GeneticTrainer
    {
        public Mutater Mutater { get; }

        public GeneticTrainer(Mutater mutater)
        {
            Mutater = mutater;
        }

        public static INeuralNetwork GetTheBestOne(IList<INeuralNetwork> neuralNetworks)
        {
            return OrderList(neuralNetworks).First();
        }

        public void Mutate(IList<INeuralNetwork> neuralNetworks)
        {
            List<INeuralNetwork> orderedNeuralNetworks = OrderList(neuralNetworks);

            Itarate(orderedNeuralNetworks);
        }

        private static List<INeuralNetwork> OrderList(IList<INeuralNetwork> neuralNetworks)
        {
            return neuralNetworks.OrderBy(x => x.Fitness).ToList();
        }

        private void Itarate(List<INeuralNetwork> orderedNeuralNetworks)
        {
            for (var i = 1; i < orderedNeuralNetworks.Count; i++)
            {
                Mutate(orderedNeuralNetworks[i], orderedNeuralNetworks[0]);
            }
        }

        private void Mutate(INeuralNetwork target, INeuralNetwork source)
        {
            Mutater.Mutate(target, source);
        }
    }
}
