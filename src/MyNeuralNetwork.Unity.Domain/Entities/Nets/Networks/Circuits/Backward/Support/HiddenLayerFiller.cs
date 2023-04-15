using MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics;
using MyNeuralNetwork.Domain.Entities.Nets.Layers;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons;

namespace MyNeuralNetwork.Domain.Entities.Nets.Networks.Circuits.Backward.Support
{
    public class HiddenLayerFiller
    {

        public static void UpdateValuesIfTheresANextLayer(Layer layer)
        {
            var nextLayer = layer.NextLayer;

            if (nextLayer != null)
            {
                IterateNeurons(layer, nextLayer);
            }
        }

        private static void IterateNeurons(Layer layer, Layer nextLayer)
        {
            layer.Neurons.ForEach(myNeuron =>
            {
                NeuralDoubleValue gammaSum = new NeuralDoubleValue();

                nextLayer.Neurons.ForEach(itsNeuron =>
                {
                    UpdateWeightAndBias(myNeuron, itsNeuron);

                    gammaSum.Increment(myNeuron.Synapses.GetWeightFor(itsNeuron) * itsNeuron.Gamma);
                });

                myNeuron.UpdateGamma(gammaSum);
            });
        }

        private static void UpdateWeightAndBias(Neuron myNeuron, Neuron itsNeuron)
        {
            myNeuron.Bias -= itsNeuron.Gamma * myNeuron.LearningRate;
            myNeuron.Synapses.GetWeightFor(itsNeuron).Decrement(CalculateGammaDecrement(myNeuron, itsNeuron));
        }

        private static double CalculateGammaDecrement(Neuron myNeuron, Neuron itsNeuron)
        {
            return itsNeuron.Gamma * myNeuron.LearningRate * myNeuron.Value.Value;
        }
    }
}
