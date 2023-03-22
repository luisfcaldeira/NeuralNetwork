using System;

namespace MyNeuralNetwork.Domain.Entities.Support
{
    public static class MyRandom
    {

        public static double Range(double v, double v1)
        {
            var _random = new Random();
            return ApplyFormula(v, v1, (double)_random.NextDouble());
        }

        private static double ApplyFormula(double v1, double v2, double rand)
        {
            return (v2 - v1) * rand + v1;
        }

    }
}
