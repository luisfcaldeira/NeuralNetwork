using MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Outputs;
using MyNeuralNetwork.Domain.Entities.Nets.Layers;
using MyNeuralNetwork.Domain.Entities.Nets.Networks;
using MyNeuralNetwork.Domain.Interfaces.Services.Circuits.Forward;

namespace Infra.Services.Circuits.Forward
{
    public class FeedForward : ICircuitForward
    {
        public void Send(NeuralNetwork neuralNetwork, Input[] inputs)
        {
            foreach(var layer in neuralNetwork.GetNextLayer())
            {
                layer.Send(inputs);

                FeedNextLayer(layer);
            }
        }

        private void FeedNextLayer(Layer layer)
        {
            var nextLayer = layer.NextLayer;

            if (nextLayer == null)
            {
                layer.Neurons.ForEach(myNeuron =>
                {
                    FloatNeuralValue output = new Output();

                    nextLayer.Neurons.ForEach(itsNeuron =>
                    {
                        output = output + myNeuron.Synapses.GetOutput(itsNeuron);
                        itsNeuron.Feed(output);
                    });
                });
            }
        }
    }
}
