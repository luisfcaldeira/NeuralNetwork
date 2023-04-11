using MyNeuralNetwork.Domain.Dtos.Entities.Nets.Layers;
using MyNeuralNetwork.Domain.Dtos.Entities.Nets.Networks;
using MyNeuralNetwork.Domain.Dtos.Entities.Nets.Neurons;
using MyNeuralNetwork.Domain.Entities.Nets.Collections.Layers;
using MyNeuralNetwork.Domain.Entities.Nets.Layers;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts;
using MyNeuralNetwork.Domain.Interfaces.Generators;

namespace MyNeuralNetwork.Domain.Entities.Nets.Conversors
{
    public class DtoToNeuralNetwork
    {
        private readonly ILayersLinker _layersLinker;

        public DtoToNeuralNetwork(ILayersLinker layersLinker)
        {
            _layersLinker = layersLinker;
        }

        public LayerCollection GenerateLayerCollection(NeuralNetworkDto neuralNetworkDto)
        {
            var layerCounter = new LayerCounter();
            var actualLayer = 0;

            var layerCollection = _layersLinker.Generate(neuralNetworkDto.Layers.Count, (i, layerCounter) =>
            {
                var layer = neuralNetworkDto.Layers[i];

                CheckLayerLabel(actualLayer, layer);

                actualLayer++;

                var neuronsDto = layer.Neurons;

                var neuronGenerator = new NeuronGenerator();

                var neurons = neuronGenerator.Generate<SynapseManager>(neuronsDto);

                FixIdentity(neuronsDto, neurons);

                return new Layer(layerCounter, neurons);
            });

            for (int i = 0; i < neuralNetworkDto.Layers.Count; i++)
            {
                var layer = layerCollection[i];
                var layerDto = neuralNetworkDto.Layers[i];

                var neuronsDto = layerDto.Neurons;
                layer.Neurons.ForEach(n => n.Synapses.ChangeWeights(neuronsDto));
            }

            return layerCollection;
        }

        private static void FixIdentity(System.Collections.Generic.List<NeuronDto> neuronsDto, Collections.Neurons.NeuronCollection neurons)
        {

            for (int j = 0; j < neurons.Count; j++)
            {
                neurons[j].ChangeIndex(neuronsDto[j].Index);
            }
        }

        private static void CheckLayerLabel(int actualLayer, LayerDto layer)
        {
            if (layer.Label != actualLayer)
            {
                throw new System.Exception($"Layers doesn't match. It should be #{actualLayer} but #{layer.Label} was found.");
            }
        }
    }
}
