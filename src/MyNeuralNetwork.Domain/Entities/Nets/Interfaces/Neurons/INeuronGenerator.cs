using MyNeuralNetwork.Domain.Entities.Nets.Collections.Neurons;
using MyNeuralNetwork.Domain.Entities.Nets.Interfaces.Neurons.Activations;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts;

namespace MyNeuralNetwork.Domain.Entities.Nets.Interfaces.Neurons
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
