using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Tests.Nets
{
    internal class NeuronCreator
    {
        private int _quantity;

        public NeuronCreator(int quantity)
        {
            _quantity = quantity;
        }

        public List<Neuron> GetNeurons()
        {
            var list =  new List<Neuron>();
            for(var i = 0; i < _quantity; i++)
            {
                list.Add(new Neuron());
            }

            return list;
        }
    }
}
