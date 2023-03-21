using MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics;
using MyNeuralNetwork.Domain.Entities.Nets.Interfaces.Networks.Circuits.Forward;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Outputs;
using MyNeuralNetwork.Domain.Entities.Nets.Layers;

namespace MyNeuralNetwork.Domain.Entities.Nets.Networks.Circuits.Forward
{
    public class FeedForward : ICircuitForward
    {
        public void Send(NeuralNetwork neuralNetwork, Input[] inputs)
        {
            foreach (var layer in neuralNetwork.GetNextLayer())
            {
                if(layer.Label == 0)
                {
                    layer.Add(inputs);
                }

                FeedNextLayer(layer);
                layer.UpdateOutput();
            }
        }

        private void FeedNextLayer(Layer layer)
        {
            var nextLayer = layer.NextLayer;

            if (nextLayer != null)
            {
                nextLayer.Neurons.ForEach(itsNeuron =>
                {

                    NeuralFloatValue output = new Output();

                    layer.Neurons.ForEach(myNeuron =>
                    {
                        myNeuron.Synapses.TransmitTo(itsNeuron);
                    });

                    itsNeuron.Commit();
                });
            }
        }
    }
}
