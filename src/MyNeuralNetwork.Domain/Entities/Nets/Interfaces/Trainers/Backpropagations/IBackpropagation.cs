using MyNeuralNetwork.Domain.Entities.Nets.Networks;

namespace MyNeuralNetwork.Domain.Entities.Nets.Interfaces.Trainers.Backpropagations
{
    public interface IBackpropagation
    {
        void Propagate(NeuralNetwork network);
    }
}
