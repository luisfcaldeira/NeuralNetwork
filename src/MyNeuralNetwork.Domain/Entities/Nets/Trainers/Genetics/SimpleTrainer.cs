using MyNeuralNetwork.Domain.Interfaces.Networks;
using MyNeuralNetwork.Domain.Interfaces.Trainers.Genetics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyNeuralNetwork.Domain.Entities.Nets.Trainers.Genetics
{
    public class SimpleTrainer : TrainerCore, IGeneticTrainer
    {
        private readonly bool inverter;

        public SimpleTrainer(Mutater mutater, bool inverter = false) : base(mutater)
        {
            this.inverter = inverter;
        }

        public INeuralNetwork GetTheBestOne(IList<INeuralNetwork> neuralNetworks)
        {
            return Rule(neuralNetworks).First();
        }

        public void ToMutate(IList<INeuralNetwork> neuralNetworks)
        {
            var sortedNeuralNetworks = Rule(neuralNetworks).ToList();

            Iterate(sortedNeuralNetworks, () => Rule(neuralNetworks).First());
        }

        private IList<INeuralNetwork> Rule(IList<INeuralNetwork> neuralNetworks)
        {
            if(inverter)
               return neuralNetworks.OrderByDescending(n => n.Fitness).ToList();

            return neuralNetworks.OrderBy(n => n.Fitness).ToList();
        }

    }
}
