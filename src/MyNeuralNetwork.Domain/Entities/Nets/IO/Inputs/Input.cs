using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs
{
    public class Input
    {
        public float Value { get; private set; }

        public Input(float value)
        {
            Value = value;
        }
    }
}
