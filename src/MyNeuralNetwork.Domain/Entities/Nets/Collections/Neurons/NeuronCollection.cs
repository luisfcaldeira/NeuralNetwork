﻿using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons;
using System;
using System.Collections.Generic;

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
        private void CheckInputSizeOrThrowError(Input[] inputs)
        {
            if (inputs.Length != Count)
            {
                throw new ArgumentException($"Size of '{nameof(inputs)}' must be the same as quantity of neurons.");
            }
        }
    }
}
