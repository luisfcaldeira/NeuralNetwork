using MyNeuralNetwork.Domain.Interfaces.Networks;
using System.Collections.Generic;

namespace MyNeuralNetwork.Domain.Interfaces.Trainers.Genetics
{
    public interface IGeneticTrainer
    {
        void ToMutate(IList<INeuralNetwork> neuralNetworks);
        INeuralNetwork GetTheBestOne(IList<INeuralNetwork> neuralNetworks);
    }
}
