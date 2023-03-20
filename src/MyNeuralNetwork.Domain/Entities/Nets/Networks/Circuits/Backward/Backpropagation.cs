using MyNeuralNetwork.Domain.Entities.Nets.Interfaces.Networks.Circuits.Backward;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Outputs;
using MyNeuralNetwork.Domain.Entities.Nets.Networks.Circuits.Backward.Support;

namespace MyNeuralNetwork.Domain.Entities.Nets.Networks.Circuits.Backward
{
    public class Backpropagation : ICircuitBackward
    {
        public void Send(NeuralNetwork neuralNetwork, Expected[] expecteds)
        {
            foreach (var layer in neuralNetwork.GetBackLayers())
            {
                LastLayerFiller.UpdateLayerIfItsLastOne(expecteds, layer);

                HiddenLayerFiller.UpdateValuesIfTheresANextLayer(layer);
            }
        }
    }
}
