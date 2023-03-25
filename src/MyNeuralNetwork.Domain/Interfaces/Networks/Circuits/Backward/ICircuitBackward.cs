using MyNeuralNetwork.Domain.Entities.Nets.IO.Outputs;
using MyNeuralNetwork.Domain.Entities.Nets.Networks;

namespace MyNeuralNetwork.Domain.Interfaces.Networks.Circuits.Backward
{
    public interface ICircuitBackward
    {
        void Send(NeuralNetwork neuralNetwork, Expected[] expecteds);
    }
}
