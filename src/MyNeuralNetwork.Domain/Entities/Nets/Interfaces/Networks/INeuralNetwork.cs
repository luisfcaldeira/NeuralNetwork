using MyNeuralNetwork.Domain.Entities.Nets.Collections.Layers;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;

namespace MyNeuralNetwork.Domain.Entities.Nets.Interfaces.Networks
{
    public interface INeuralNetwork
    {
        LayerCollection Layers { get; }
        float[] Predict(Input[] inputs);
    }
}
