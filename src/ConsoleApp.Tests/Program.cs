using ConsoleApp.Tests.Nets;
using System;
using System.Diagnostics;

namespace ConsoleApp.Tests
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var myLayers = new int[] { 5, 3, 2 };
            
            var layer = new Layer(myLayers);
            myLayers[0] = 1000;
            layer.PrintLayers();
            layer.PrintNeurons();

            var neuralNetwork = new NeuralNetwork(myLayers);

            Debugger.Break();
        }
    }
}
