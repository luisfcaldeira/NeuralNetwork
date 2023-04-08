using MyNeuralNetwork.Domain.Interfaces.Networks;
using System.Collections.Generic;
using System.Linq;

namespace MyNeuralNetwork.Domain.Entities.Nets.Trainers.Genetics
{
    public class GeneticTrainer
    {

        public GeneticTrainer(double chance)
        {
            Mutater.ChanceOfMutate = chance;
        }

        public void Mutate(IList<INeuralNetwork> neuralNetworks)
        {
            var orderedNeuralNetworks = neuralNetworks.OrderByDescending(x => x.Fitness).ToList();

            for(var i = 1; i < orderedNeuralNetworks.Count(); i++)
            {
                Mutater.Mutate(orderedNeuralNetworks[i], orderedNeuralNetworks[0]);
            }
        }
    }
}
