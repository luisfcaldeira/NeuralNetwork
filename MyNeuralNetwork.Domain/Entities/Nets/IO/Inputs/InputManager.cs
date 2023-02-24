using MyNeuralNetwork.Domain.Entities.Nets.Collections.IO.Inputs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs
{
    public class InputManager
    {
        public InputCollection Inputs { get; private set; }

        public InputManager()
        {
            Inputs = new InputCollection();
        }

        public void AddInput(float input)
        {
            Inputs.Add(new Input(input));
        }

    }
}
