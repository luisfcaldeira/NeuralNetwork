using Microsoft.Win32;
using MyNeuralNetwork.Domain.Entities.Nets.Collections.IO.Outputs;
using MyNeuralNetwork.Domain.Entities.Nets.Collections.Layers;
using MyNeuralNetwork.Domain.Entities.Nets.Collections.Neurons;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Outputs;
using MyNeuralNetwork.Domain.Entities.Nets.Layers;
using MyNeuralNetwork.Domain.Entities.Nets.Networks;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Activations;
using System;
using System.Diagnostics;
using System.Media;

namespace ConsoleApp.Tests
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var inputManager = new InputManager();
            inputManager.AddInput(0.01f);
            inputManager.AddInput(0.01f);
            var expecteds = new ExpectedCollection();
            expecteds.Add(new Expected(0.02f));

            NeuralNetwork neuralNetwork = GenerateNeuralNetwork();
            ExampleNeuralNetwork exampleNeuralNetwork = new ExampleNeuralNetwork(new int[] { 2, 5, 1 }, new string[] { "tanh", "tanh", "tanh" });

            Fit(inputManager, expecteds, neuralNetwork);

            PrintResult(neuralNetwork, exampleNeuralNetwork);

            for (var i = 0; i < 100000; i++)
            {
                Fit(exampleNeuralNetwork);
                Fit(inputManager, expecteds, neuralNetwork);
            }

            //Fit(inputManager, expecteds, neuralNetwork);
            PrintResult(neuralNetwork, exampleNeuralNetwork);

            PlayNotificationSound();

            inputManager = new InputManager();
            inputManager.AddInput(0.08f);
            inputManager.AddInput(0.08f);

            var result = neuralNetwork.Predict(inputManager.Inputs);
            Console.WriteLine("\n-- result --");
            Console.WriteLine(result);
        }

        
        private static void Fit(InputManager inputManager, ExpectedCollection expecteds, NeuralNetwork neuralNetwork)
        {
            neuralNetwork.Fit(inputManager.Inputs);
            neuralNetwork.Backpropagate(expecteds);
        }

        private static void Fit(ExampleNeuralNetwork exampleNeuralNetwork)
        {
            exampleNeuralNetwork.FeedForward(new float[] { 0.01f, 0.01f });
            exampleNeuralNetwork.BackPropagate(new float[] { 0.01f, 0.01f }, new float[] { 0.02f });
        }

        private static void PrintResult(NeuralNetwork neuralNetwork, ExampleNeuralNetwork exampleNeuralNetwork)
        {
            Console.WriteLine("-------------");
            neuralNetwork.PrintLayers();
            Console.WriteLine("- ex:");
            PrintOutput(exampleNeuralNetwork);
        }

        private static void PrintOutput(ExampleNeuralNetwork exampleNeuralNetwork)
        {
            var output = exampleNeuralNetwork.FeedForward(new float[] { 0.01f, 0.01f });
            foreach (var o in output)
            {
                Console.WriteLine(o);
            }

            Console.WriteLine("Neurons: ");
            foreach(var n in exampleNeuralNetwork.neurons)
            {
                Console.WriteLine("");
                var comma = "";
                foreach(var o in n)
                {
                    Console.Write(comma + o);
                    comma = ", ";
                }
            }
        }

        private static NeuralNetwork GenerateNeuralNetwork()
        {
            var layers = new LayerCollection();

            Layer layer1 = new Layer(GenerateNeurons(2));
            Layer layer2 = new Layer(GenerateNeurons(5));
            Layer layer3 = new Layer(GenerateNeurons(1));

            layer1.NextLayer = layer2;
            layer2.NextLayer = layer3;

            layer2.PreviousLayer = layer1;
            layer3.PreviousLayer = layer2;

            layers.Add(layer1);
            layers.Add(layer2);
            layers.Add(layer3);
            return new NeuralNetwork(layers);
        }

        private static NeuronCollection GenerateNeurons(int v)
        {
            var neurons = new NeuronCollection();
            for(var i = 0; i < v; i++)
            {
                neurons.Add(new Neuron(new Tanh()) { LearningRate = 0.01f });
            }
            return neurons;
        }

        public static void PlayNotificationSound()
        {
            bool found = false;
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"AppEvents\Schemes\Apps\.Default\Notification.Default\.Current"))
                {
                    if (key != null)
                    {
                        Object o = key.GetValue(null); // pass null to get (Default)
                        if (o != null)
                        {
                            SoundPlayer theSound = new SoundPlayer((String)o);
                            theSound.Play();
                            found = true;
                        }
                    }
                }
            }
            catch
            { }
            if (!found)
                SystemSounds.Beep.Play(); // consolation prize
        }
    }
}
