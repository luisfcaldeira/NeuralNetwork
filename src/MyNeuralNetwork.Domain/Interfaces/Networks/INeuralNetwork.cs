using MyNeuralNetwork.Domain.Entities.Nets.Collections.Layers;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;

namespace MyNeuralNetwork.Domain.Interfaces.Networks
{
    public interface INeuralNetwork
    {
        LayerCollection Layers { get; }
        double[] Predict(Input[] inputs);
    }
}
