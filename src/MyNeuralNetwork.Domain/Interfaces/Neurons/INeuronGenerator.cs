using MyNeuralNetwork.Domain.Entities.Nets.Collections.Neurons;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts;
using MyNeuralNetwork.Domain.Interfaces.Neurons.Activations;

namespace MyNeuralNetwork.Domain.Interfaces.Neurons
{
    public interface INeuronGenerator
    {
        double LearningRate { get; set; }
        RangeConfiguration WeightConfiguration { get; set; }
        RangeConfiguration BiasConfiguration { get; set; }

        NeuronCollection Generate<ISynapseManagerImplementation, IActivatorImplementation>(int quantity);
        NeuronCollection Generate<ISynapseManagerImplementation>(int quantity, IActivator activator);
    }
}
