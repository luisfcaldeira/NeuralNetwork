﻿using MyNeuralNetwork.Domain.Entities.Support;

namespace MyNeuralNetwork.Domain.Entities.Nets
{
    public class Weight
    {
        private readonly float _learningRate;

        public float Value { get; set; }

        public Weight(float learningRate)
        {
            Value = MyRandom.Range(-0.5f, 0.5f);
            Value = 1;
            _learningRate = learningRate;
        }

        internal void Fix(float newGamma)
        {
            Value -= newGamma;
        }
    }
}
