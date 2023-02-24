using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using System.Collections.Generic;

namespace MyNeuralNetwork.Domain.Entities.Nets.Collections.IO.Inputs
{
    public class InputCollection : List<Input>
    {
        public void Add(float value)
        {
            Add(new Input(value));
        }

    }
}
