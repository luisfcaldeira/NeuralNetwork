using MyNeuralNetwork.Domain.Dtos.Entities.Nets.Networks;
using MyNeuralNetwork.Domain.Entities.Nets.Networks;

namespace MyNeuralNetwork.Domain.Interfaces.Services.Persistences
{
    public interface INeuralNetworkPersistence
    {
        string Path { get; set; }
        string FileName { get; set; }
        string FullPath { get; }
        void Save(NeuralNetwork neuralNetwork);
        NeuralNetworkDto Load();
    }
}
