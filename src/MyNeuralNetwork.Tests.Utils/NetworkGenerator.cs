﻿using MyNeuralNetwork.Domain.Entities.Nets.Generators;
using MyNeuralNetwork.Domain.Entities.Nets.Generators.Supports;
using MyNeuralNetwork.Domain.Entities.Nets.Networks;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Activations;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts;
using MyNeuralNetwork.Tests.Utils.Activations;

namespace MyNeuralNetwork.Tests.Utils
{
    public class NetworkGenerator
    {

        public static double FixedMaxWeight = 1;
        public static double FixedMinWeight = 1;
        public static double FixedMaxBias = 0;
        public static double FixedMinBias = 0;

        public static NeuralNetwork GiveMeOne(int[] sizeOfLayers, bool random = true)
        {
            var neuronGenerator = new Domain.Entities.Nets.Neurons.NeuronGenerator();
            if (!random)
            {
                neuronGenerator.WeightConfiguration.SetMaxAndMin(FixedMinWeight, FixedMaxWeight);
                neuronGenerator.BiasConfiguration.SetMaxAndMin(FixedMinBias, FixedMaxBias);
            }

            var nNGenerator = new NNGenerator(neuronGenerator, new LayersLinker());
            var network = nNGenerator.Generate<SynapseManager, ActivationTester>(sizeOfLayers);

            return network;
        }

        public static NeuralNetwork GiveMeOneSigmoid(int[] sizeOfLayers, bool random = true)
        {
            var neuronGenerator = new Domain.Entities.Nets.Neurons.NeuronGenerator();
            if (!random)
            {
                neuronGenerator.WeightConfiguration.SetMaxAndMin(FixedMinWeight, FixedMaxWeight);
                neuronGenerator.BiasConfiguration.SetMaxAndMin(FixedMinBias, FixedMaxBias);
            }

            var nNGenerator = new NNGenerator(neuronGenerator, new LayersLinker());
            var network = nNGenerator.Generate<SynapseManager, Sigmoid>(sizeOfLayers);

            return network;
        }

        public static NeuralNetwork GiveMeOneTanh(int[] sizeOfLayers, bool random = true)
        {
            var neuronGenerator = new Domain.Entities.Nets.Neurons.NeuronGenerator();
            if (!random)
            {
                neuronGenerator.WeightConfiguration.SetMaxAndMin(FixedMinWeight, FixedMaxWeight);
                neuronGenerator.BiasConfiguration.SetMaxAndMin(FixedMinBias, FixedMaxBias);
            }

            var nNGenerator = new NNGenerator(neuronGenerator, new LayersLinker());
            var network = nNGenerator.Generate<SynapseManager, Tanh>(sizeOfLayers);

            return network;
        }
    }
}
