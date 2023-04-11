using MyNeuralNetwork.Domain.Interfaces.Networks;
using System;
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
            return Roulette(neuralNetworks).First();
        }

        public void ToMutate(IList<INeuralNetwork> neuralNetworks)
        {
            List<INeuralNetwork> orderedNeuralNetworks = Roulette(neuralNetworks);

            Iterate(orderedNeuralNetworks);
        }

        private static List<INeuralNetwork> Roulette(IList<INeuralNetwork> neuralNetworks)
        {
            var sumOfFitnesses = neuralNetworks.Select(x => x.Fitness).Sum();

            return neuralNetworks.OrderBy(x => Math.Abs(x.Fitness / sumOfFitnesses)).ToList();
        }

        private void Iterate(List<INeuralNetwork> orderedNeuralNetworks)
        {
            for (var i = 1; i < orderedNeuralNetworks.Count; i++)
            {
                Mutate(orderedNeuralNetworks[i], GetOneOfTwoParents(orderedNeuralNetworks));
            }
        }

        private static INeuralNetwork GetOneOfTwoParents(List<INeuralNetwork> orderedNeuralNetworks)
        {
            return orderedNeuralNetworks.Take(2).OrderBy(x => Guid.NewGuid()).First();
        }

        private void Mutate(INeuralNetwork target, INeuralNetwork source)
        {
            Mutater.Mutate(target, source);
        }
    }
}
