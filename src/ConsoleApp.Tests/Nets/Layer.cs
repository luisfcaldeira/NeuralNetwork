using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Tests.Nets
{
    internal class Layer
    {
        public List<int> Layers { get; }
        public float[][] Neurons { get; private set; }

        public Layer(int[] layers)
        {
            Layers = new List<int>(layers);
            InitNeurons();
        }


        private void InitNeurons()
        {
            List<float[]> neuronsList = new List<float[]>();
            for (int i = 0; i < Layers.Count; i++)
            {
                neuronsList.Add(new float[Layers[i]]);
            }
            Neurons = neuronsList.ToArray();
        }

        public void PrintLayers()
        {
            foreach (var layer in Layers)
            {
                Console.WriteLine(layer);
            }
        }

        public void PrintNeurons()
        {
            foreach (var neuron in Neurons)
            {
                var com = "";
                Console.Write("[ ");
                foreach(var n in neuron)
                {
                    Console.Write(com + n);
                    com = ", ";
                }
                Console.WriteLine(" ]");
            }
        }

    }
}
