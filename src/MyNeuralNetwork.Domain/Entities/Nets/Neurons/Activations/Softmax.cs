using MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Interfaces.Neurons.Activations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyNeuralNetwork.Domain.Entities.Nets.Neurons.Activations
{
    public class Softmax : IActivator
    {
        private static int ids = 1;
        private int myId = ids++;

        private double sums = 0;

        public double Activate(double x)
        {
            if (myId == 1)
            {
                sums = 0;
            }
            double result = Math.Exp(x);
            sums += result; 
            return result / sums;
        }

        public double Derivative(double x)
        {
            double softmax = Activate(x);
            return softmax * (1 - softmax);
        }

        public NeuralDoubleValue Activate(NeuralDoubleValue x)
        {
            return new NeuralDoubleValue(Activate(x.Value));
        }

        public NeuralDoubleValue Derivative(NeuralDoubleValue x)
        {
            return new NeuralDoubleValue(Derivative(x.Value));
        }
    }
}
