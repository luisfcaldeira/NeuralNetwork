using MyNeuralNetwork.Domain.Entities.Nets.Collections.Neurons;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts;

namespace MyNeuralNetwork.Domain.Entities.Nets.Interfaces.Neurons
{
    public interface INeuronGenerator
    {
        RangeConfiguration WeightConfiguration { get; set; }
        RangeConfiguration BiasConfiguration { get; set; }

        NeuronCollection Generate<ISynapseManagerImplementation, IActivatorImplementation>(int quantity);
    }
}
