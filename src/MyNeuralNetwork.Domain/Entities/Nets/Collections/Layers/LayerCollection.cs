using MyNeuralNetwork.Domain.Entities.Nets.Layers;
using System.Collections.Generic;

namespace MyNeuralNetwork.Domain.Entities.Nets.Collections.Layers
{
    public class LayerCollection : List<Layer>
    {
        public LayerCollection() { }

        internal LayerCollection(Layer[] layers) : base(layers) { }
       
        internal LayerCollection Copy()
        {
            return new LayerCollection(ToArray());
        }
    }
}
