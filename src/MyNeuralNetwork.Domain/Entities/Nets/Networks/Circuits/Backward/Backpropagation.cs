using MyNeuralNetwork.Domain.Entities.Nets.IO.Outputs;
using MyNeuralNetwork.Domain.Entities.Nets.Networks.Circuits.Backward.Support;
using MyNeuralNetwork.Domain.Interfaces.Networks.Circuits.Backward;

namespace MyNeuralNetwork.Domain.Entities.Nets.Networks.Circuits.Backward
{
    public class Backpropagation : ICircuitBackward
    {
        public void Send(NeuralNetwork neuralNetwork, Expected[] expecteds)
        {
            neuralNetwork.Layers.ForEach(l =>
            {
                l.Neurons.ForEach(n =>
                {
                    n.Gamma = 0;
                });
            });

            foreach (var layer in neuralNetwork.GetBackLayers())
            {
                LastLayerFiller.UpdateLayerIfItsLastOne(expecteds, layer);

                HiddenLayerFiller.UpdateValuesIfTheresANextLayer(layer);
            }
        }
    }
}
