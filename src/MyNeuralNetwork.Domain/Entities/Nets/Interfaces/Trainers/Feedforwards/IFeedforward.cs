using MyNeuralNetwork.Domain.Entities.Nets.Networks;

namespace MyNeuralNetwork.Domain.Entities.Nets.Interfaces.Trainers.Feedforwards
{
    public interface IFeedforward
    {
        void Feed(NeuralNetwork myNeuralNetwork);
    }
}
