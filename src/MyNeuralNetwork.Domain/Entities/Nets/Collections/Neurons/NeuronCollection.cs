using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyNeuralNetwork.Domain.Entities.Nets.Collections.Neurons
{
    public class NeuronCollection : List<Neuron>
    {
        public void Feed(Input[] inputs)
        {
            CheckInputSizeOrThrowError(inputs);

            for (var i = 0; i < Count; i++)
            {
                this[i].Feed(inputs[i]);
            }
        }

        public float SumOutputDotWeight(NeuronCollection neurons)
        {
            return this.Sum(myNeuron => {
                float result = 0;
                neurons.ForEach(theirNeuron =>
                {
                    result =+ myNeuron.GetOutput(theirNeuron).Value * myNeuron.Synapses.GetSynapse(theirNeuron).Weight;
                });
                return result;
            });
        }

        public float SumGammaDotFloat(float multiplier)
        {
            return this.Sum(n => n.Gamma * multiplier);
        }

        internal float SumGammaDotWeigth(NeuronCollection neurons)
        {
            float result = 0;

            ForEach(myNeuron => 
            {
                neurons.ForEach(theirNeuron =>
                {
                    result = + myNeuron.Gamma * myNeuron.Synapses.GetSynapse(theirNeuron).Weight;
                });
            });

            return result;
        }

        internal void Predict(Input[] inputs)
        {
            CheckInputSizeOrThrowError(inputs);
        }

        private void CheckInputSizeOrThrowError(Input[] inputs)
        {
            if (inputs.Length != Count)
            {
                throw new ArgumentException($"Size of '{nameof(inputs)}' must be the same as quantity of neurons.");
            }

            for (var i = 0; i < Count; i++)
            {
                this[i].Predict(inputs[i]);
            }
        }

    }
}
