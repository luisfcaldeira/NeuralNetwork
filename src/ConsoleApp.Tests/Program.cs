using MyNeuralNetwork.Domain.Entities.Nets.Generators;
using MyNeuralNetwork.Domain.Entities.Nets.Generators.Supports;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Managers;
using MyNeuralNetwork.Domain.Entities.Nets.Networks.Circuits.Backward;
using MyNeuralNetwork.Domain.Entities.Nets.Networks.Circuits.Forward;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Activations;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts;
using MyNeuralNetwork.Domain.Entities.Nets.Trainers;
using MyNeuralNetwork.Domain.Interfaces.Networks;
using System;

namespace ConsoleApp.Example
{
    internal class Program
    {
        static void Main()
        {
            var ngen = new NeuronGenerator();
            ngen.WeightConfiguration.SetMaxAndMin(-1, 1);
            ngen.BiasConfiguration.SetMaxAndMin(-1, 1);

            var nngen = new NNGenerator(ngen, new LayersLinker());

            var neuralNetwork = nngen.Generate<SynapseManager, Tanh>(new int[] { 2, 18, 1 });

            var dataManager = GenDataForAndGate();

            Console.WriteLine("Neural Network for AND Gate. ");
            Console.WriteLine($"Results before training: ");

            Predict(neuralNetwork);

            var trainer = new Trainer(dataManager, neuralNetwork, new FeedForward(), new Backpropagation());

            trainer.Fit(5000);

            Console.WriteLine($"\nResults after training: ");

            Predict(neuralNetwork);
        }

        private static DataManager GenDataForAndGate()
        {
            var dataManager = new DataManager();

            // T ^ T (T)
            dataManager.Inputs(2)
                .AddInput(1)
                .AddInput(1);
            dataManager.Expecteds(1)
                .AddExpected(1);

            // T ^ F (F)
            dataManager.Inputs(2)
                .AddInput(1)
                .AddInput(0);
            dataManager.Expecteds(1)
                .AddExpected(0);

            // F ^ T (F)
            dataManager.Inputs(2)
                .AddInput(0)
                .AddInput(1);
            dataManager.Expecteds(1)
                .AddExpected(0);

            // F ^ F (F)
            dataManager.Inputs(2)
                .AddInput(0)
                .AddInput(0);
            dataManager.Expecteds(1)
                .AddExpected(0);
            return dataManager;
        }

        private static void Predict(INeuralNetwork neuralNetwork)
        {
            var result = neuralNetwork.Predict(new Input[] { new Input(1), new Input(1) });
            Console.WriteLine($"T ^ T = {ConvertResult(result)}");

            result = neuralNetwork.Predict(new Input[] { new Input(1), new Input(0) });
            Console.WriteLine($"T ^ F = {ConvertResult(result)}");

            result = neuralNetwork.Predict(new Input[] { new Input(0), new Input(1) });
            Console.WriteLine($"F ^ T = {ConvertResult(result)}");

            result = neuralNetwork.Predict(new Input[] { new Input(0), new Input(0) });
            Console.WriteLine($"F ^ F = {ConvertResult(result)}");
        }

        private static bool ConvertResult(double[] result)
        {
            return Convert.ToBoolean(Math.Round(result[0], 0));
        }

    }
}
