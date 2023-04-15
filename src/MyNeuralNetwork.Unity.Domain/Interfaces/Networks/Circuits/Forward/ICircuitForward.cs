using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.Networks;

namespace MyNeuralNetwork.Domain.Interfaces.Networks.Circuits.Forward
{
    public interface ICircuitForward
    {
        void Send(NeuralNetwork neuralNetwork, Input[] inputs);
    }
}
