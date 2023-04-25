using MyNeuralNetwork.Domain.Interfaces.Networks;
using MyNeuralNetwork.Domain.Interfaces.Trainers.Genetics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyNeuralNetwork.Domain.Entities.Nets.Trainers.Genetics
{
    public class RouletteTrainer : TrainerCore, IGeneticTrainer
    {
        public RouletteTrainer(Mutater mutater) :base(mutater) { }


        public INeuralNetwork GetTheBestOne(IList<INeuralNetwork> neuralNetworks)
        {
            return Roulette(neuralNetworks).First();
        }

        private static List<INeuralNetwork> Roulette(IList<INeuralNetwork> neuralNetworks)
        {
            var sumOfFitnesses = neuralNetworks.Select(x => x.Fitness).Sum();

            return neuralNetworks.OrderBy(x => Math.Abs(x.Fitness / sumOfFitnesses)).ToList();
        }

        public void ToMutate(IList<INeuralNetwork> neuralNetworks)
        {
            List<INeuralNetwork> orderedNeuralNetworks = Roulette(neuralNetworks);

            Iterate(orderedNeuralNetworks,() => GetOneOfTwoParents(orderedNeuralNetworks) );
        }

        private static INeuralNetwork GetOneOfTwoParents(List<INeuralNetwork> orderedNeuralNetworks)
        {
            return orderedNeuralNetworks.Take(2).OrderBy(x => Guid.NewGuid()).First();
        }

    }
}
