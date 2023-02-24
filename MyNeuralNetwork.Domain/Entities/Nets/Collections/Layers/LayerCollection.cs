using MyNeuralNetwork.Domain.Entities.Nets.Collections.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.Collections.IO.Outputs;
using System.Collections.Generic;
using System.Linq;

namespace MyNeuralNetwork.Domain.Entities.Nets.Collections.Layers
{
    public class LayerCollection : List<Layer>
    {
        public void FeedForward(InputCollection inputs)
        {
            if(Count > 0)
            {
                this[0].Feedforward(inputs);
            }
        }

        public void BackPropagation(ExpectedCollection expecteds)
        {
            if(Count > 0)
            {
                this.Last().BackPropagation(expecteds);
            }
        }

        public void Predict(InputCollection inputs)
        {
            if(Count > 0)
            {
                this[0].Predict(inputs);
            }
        }
    }
}
