using MyNeuralNetwork.Domain.Entities.Nets.Collections.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.Collections.IO.Outputs;
using MyNeuralNetwork.Domain.Entities.Nets.Layers;
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

        public void BackPropagate(ExpectedCollection expecteds)
        {
            if(Count > 0)
            {
                this.Last().BackPropagate(expecteds);
            }
        }

        public void Predict(InputCollection inputs)
        {
            if(Count == 0)
                throw new System.ArgumentException("There're no layers in the network.");
            
            this[0].Predict(inputs);
        }
    }
}
