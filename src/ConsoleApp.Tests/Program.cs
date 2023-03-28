using Core.Infra.IoC.Mappers;
using Core.Infra.Services.GraphPlotters;
using Core.Infra.Services.Loggers.OutputWindow;
using MyNeuralNetwork.Domain.Entities.Nets.Generators;
using MyNeuralNetwork.Domain.Entities.Nets.Generators.Supports;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Managers;
using MyNeuralNetwork.Domain.Entities.Nets.Networks;
using MyNeuralNetwork.Domain.Entities.Nets.Networks.Circuits.Backward;
using MyNeuralNetwork.Domain.Entities.Nets.Networks.Circuits.Forward;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Activations;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts;
using MyNeuralNetwork.Domain.Entities.Nets.Trainers;
using MyNeuralNetwork.Domain.Interfaces.Networks;
using MyNeuralNetwork.Domain.Interfaces.Neurons.Activations;
using MyNeuralNetwork.Domain.Interfaces.Services.Logs;
using MyNeuralNetwork.Domain.Interfaces.Services.Persistences;
using System;
using System.Collections.Generic;

namespace ConsoleApp.Tests
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] layers = new int[] { 2, 4, 1 };
            ExampleNeuralNetwork exampleNeuralNetwork = new ExampleNeuralNetwork(layers, new string[] { "tanh", "tanh", "tanh" });

            var watch = new System.Diagnostics.Stopwatch();

            watch.Start();

            const int epochs = 100;
            var traceLog = new DebuggerLog();
            for (var i = 0; i < epochs; i++)
            {
                Fit(exampleNeuralNetwork, traceLog);
            }

            watch.Stop();
            Console.WriteLine($"\nTraining Time (example code): {watch.ElapsedMilliseconds} ms");

            var neuralNetwork = Fit(epochs, layers);
            var mapperIoc = new IocMapper();

            var persistence = mapperIoc.GetService<INeuralNetworkPersistence>();
            var prediction = neuralNetwork.Predict(new Input[] { new Input(0.01f), new Input(0.01f) });

            persistence.Save((NeuralNetwork)neuralNetwork);

            Console.WriteLine("Predicion test: ");       
            Console.WriteLine(prediction[0]);

            FinishWithResults(exampleNeuralNetwork, neuralNetwork);
        }

        private static void FinishWithResults(INeuralNetwork neuralNetwork)
        {
            PrintResult(neuralNetwork);

            const int TotalOfIteractions = 100;
            var yValues = GenerateY(TotalOfIteractions);
            var myNnPredictions = Predict(neuralNetwork, TotalOfIteractions);

            var expectedData = GenerateExpectedData(TotalOfIteractions);

            GraphPlotter.PlotChart(myNnPredictions, expectedData, yValues);
        }

        private static void FinishWithResults(ExampleNeuralNetwork exampleNeuralNetwork, INeuralNetwork neuralNetwork)
        {
            PrintResult(neuralNetwork, exampleNeuralNetwork);

            const int TotalOfIteractions = 100;
            var yValues = GenerateY(TotalOfIteractions);
            var myNnPredictions = Predict(neuralNetwork, TotalOfIteractions);

            var expectedData = GenerateExpectedData(TotalOfIteractions);
            var examplePredictions = Predict(exampleNeuralNetwork, TotalOfIteractions);

            GraphPlotter.PlotChart(myNnPredictions, expectedData, yValues);
            GraphPlotter.PlotChart(examplePredictions, expectedData, yValues);
        }

        private static INeuralNetwork Fit(int epochs, int[] layers)
        {
            var dataManager = new DataManager();
            double maxIter = 1;
            var random = new Random();
            dataManager.Inputs(2).AddInput(0.01).AddInput(0.01);
            dataManager.Expecteds(1).AddExpected(0.02);

            //for (double i = 1; i <= maxIter; i += random.Next(5, 11))
            //{
            //    dataManager.Inputs(2).AddInput(i / maxIter).AddInput(i / maxIter);
            //    dataManager.Expecteds(1).AddExpected((i + i) / maxIter);
            //}

            //Console.WriteLine($"Sample size: {dataManager.InputCount()}");

            var neuronGenerator = new NeuronGenerator();
            neuronGenerator.LearningRate = 0.01f;
            neuronGenerator.WeightConfiguration.SetMaxAndMin(1, 1);
            neuronGenerator.BiasConfiguration.SetMaxAndMin(1, 1);
            
            var nNGen = new NNGenerator(neuronGenerator, new LayersLinker());
            
            var neuralNetwork = nNGen.Generate<SynapseManager>(layers, new IActivator[] {new Tanh(), new Tanh(), new Tanh() });
            var trainer = new Trainer(dataManager, neuralNetwork, new FeedForward(), new Backpropagation(), new DebuggerLog());

            trainer.Fit(epochs);

            Console.WriteLine($"\nTraining Time (my code): {trainer.TimeOfTraining} ms");

            return neuralNetwork;
        }

        private static void Fit(ExampleNeuralNetwork exampleNeuralNetwork, ITraceLog traceLog)
        {
            exampleNeuralNetwork.FeedForward(new double[] { 0.01f, 0.01f });
            exampleNeuralNetwork.BackPropagate(new double[] { 0.01f, 0.01f }, new double[] { 0.02f });
            traceLog.Log(exampleNeuralNetwork.ToString());
        }

        private static void PrintResult(INeuralNetwork neuralNetwork)
        {
            Console.WriteLine("-------------");
            Console.WriteLine(neuralNetwork);
        }


        private static void PrintResult(INeuralNetwork neuralNetwork, ExampleNeuralNetwork exampleNeuralNetwork)
        {
            Console.WriteLine("-------------");
            Console.WriteLine(neuralNetwork);
            Console.WriteLine("- ex:");
            Console.WriteLine(exampleNeuralNetwork.ToString());
            PrintOutput(exampleNeuralNetwork);
        }

        private static void PrintOutput(ExampleNeuralNetwork exampleNeuralNetwork)
        {
            var output = exampleNeuralNetwork.FeedForward(new double[] { 0.01f, 0.01f });
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

        private static List<double> GenerateY(int totalOfIteractions)
        {
            var yValues = new List<double>();

            for (double i = 0; i < totalOfIteractions; i++)
            {
                double input = i / totalOfIteractions;
                yValues.Add(input);
            }

            return yValues;
        }


        private static List<double> Predict(INeuralNetwork neuralNetwork, int totalOfIterations)
        {
            var listResult = new List<double>();

            for (double i = 0; i < totalOfIterations; i++)
            {
                double input = i / totalOfIterations;
                var inputManager = new InputInserter(2);
                inputManager.AddInput(input);
                inputManager.AddInput(input);

                var result = neuralNetwork.Predict(inputManager.Get());
                listResult.Add(result[0]);
            }

            return listResult;
        }

        private static List<double> GenerateExpectedData(int totalOfIteractions)
        {
            var expectedResults = new List<double>();

            for(double i = 0; i < totalOfIteractions; i++)
            {
                double input = i / totalOfIteractions;
                expectedResults.Add(input * 2);
            }

            return expectedResults;
        }
    }
}
