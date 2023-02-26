using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeuralNetwork.Domain.Entities.Support
{
    public static class MyRandom
    {

        public static float Range(float v, float v1)
        {
            var _random = new Random();
            return ApplyFormula(v, v1, (float)_random.NextDouble());
        }

        private static float ApplyFormula(float v1, float v2, float rand)
        {
            return (v2 - v1) * rand + v1;
        }

    }
}
