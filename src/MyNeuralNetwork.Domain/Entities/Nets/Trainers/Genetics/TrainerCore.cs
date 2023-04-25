using MyNeuralNetwork.Domain.Interfaces.Networks;
using System;
using System.Collections.Generic;

namespace MyNeuralNetwork.Domain.Entities.Nets.Trainers.Genetics
{
    public abstract class TrainerCore
    {
        public TrainerCore(Mutater mutater)
        {
            Mutater = mutater;
        }

        protected Mutater Mutater { get; }

        protected void Iterate(List<INeuralNetwork> orderedNeuralNetworks, Func<INeuralNetwork> ruleOfParent)
        {
            for (var i = 1; i < orderedNeuralNetworks.Count; i++)
            {
                Mutate(orderedNeuralNetworks[i], ruleOfParent.Invoke());
            }
        }

        protected void Mutate(INeuralNetwork target, INeuralNetwork source)
        {
            Mutater.Mutate(target, source);
        }
    }
}
