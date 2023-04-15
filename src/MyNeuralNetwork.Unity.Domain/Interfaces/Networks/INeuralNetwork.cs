using MyNeuralNetwork.Domain.Entities.Nets.Collections.Layers;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using System;

namespace MyNeuralNetwork.Domain.Interfaces.Networks
{
    public interface INeuralNetwork : IComparable<INeuralNetwork>
    {
        double Fitness { get; set; }
        LayerCollection Layers { get; }
        int CounterOfMutations { get; set; }

        double[] Predict(Input[] inputs);
    }
}
