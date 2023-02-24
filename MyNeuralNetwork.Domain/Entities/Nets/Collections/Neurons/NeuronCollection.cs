using MyNeuralNetwork.Domain.Entities.Nets.Collections.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Outputs;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyNeuralNetwork.Domain.Entities.Nets.Collections.Neurons
{
    public class NeuronCollection : List<Neuron>
    {
        public void Feed(InputCollection inputs)
        {
            CheckInputSizeOrThrowError(inputs);

            for (var i = 0; i < Count; i++)
            {
                this[i].Fit(inputs[i]);
            }
        }

        public float SumOutput()
        {
            return this.Sum(e => e.GetOutput().Value * e.Weight.Value);
        }

        public float SumGammaDotFloat(float multiplier)
        {
            return this.Sum(n => n.Gamma * multiplier);
        }

        internal void Predict(InputCollection inputs)
        {
            CheckInputSizeOrThrowError(inputs);
        }

        private void CheckInputSizeOrThrowError(InputCollection inputs)
        {
            if (inputs.Count != Count)
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
