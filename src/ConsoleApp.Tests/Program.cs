using Infra.Services.Logs.Files;
using MyNeuralNetwork.Domain.Entities.Nets.Generators;
using MyNeuralNetwork.Domain.Entities.Nets.Interfaces.Networks;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Managers;
using MyNeuralNetwork.Domain.Entities.Nets.Networks;
using MyNeuralNetwork.Domain.Entities.Nets.Networks.Circuits.Backward;
using MyNeuralNetwork.Domain.Entities.Nets.Networks.Circuits.Forward;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons;
using MyNeuralNetwork.Domain.Entities.Nets.Trainers;
using MyNeuralNetwork.Domain.Interfaces.Services.Logs;
using Plotly.NET;
using Plotly.NET.LayoutObjects;
using System;
using System.Collections.Generic;

namespace ConsoleApp.Tests
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Salvar em arquivo de texto valores dos neurônios, pesos e bias e comparar as duas redes depois de treinadas. 
            // Estão apresentando predições diferentes. 

            int[] layers = new int[] { 2, 4, 1 };
            ExampleNeuralNetwork exampleNeuralNetwork = new ExampleNeuralNetwork(layers, new string[] { "tanh", "tanh", "tanh" });

            var watch = new System.Diagnostics.Stopwatch();

            watch.Start();

            const int epochs = 1;
            var traceLog = new FileTraceLogService();
            for (var i = 0; i < epochs; i++)
            {
                Fit(exampleNeuralNetwork, traceLog);
            }

            watch.Stop();
            Console.WriteLine($"\nTraining Time (example code): {watch.ElapsedMilliseconds} ms");

            var neuralNetwork = Fit(epochs, layers);

            var prediction = neuralNetwork.Predict(new Input[] { new Input(0.01f), new Input(0.01f) });

            Console.WriteLine("Predicion test: ");
            Console.WriteLine(prediction[0]);

            FinishWithResults(exampleNeuralNetwork, neuralNetwork);
        }

        private static void FinishWithResults(ExampleNeuralNetwork exampleNeuralNetwork, NeuralNetwork neuralNetwork)
        {
            PrintResult(neuralNetwork, exampleNeuralNetwork);

            const int TotalOfIteractions = 100;
            var yValues = GenerateY(TotalOfIteractions);
            var myNnPredictions = Predict(neuralNetwork, TotalOfIteractions);

            var expectedData = GenerateExpectedData(TotalOfIteractions);
            var examplePredictions = Predict(exampleNeuralNetwork, TotalOfIteractions);

            PlotChart(myNnPredictions, expectedData, yValues);
            PlotChart(examplePredictions, expectedData, yValues);
        }

        private static NeuralNetwork Fit(int epochs, int[] layers)
        {
            var dataManager = new DataManager();
            dataManager.Inputs(2).AddInput(0.01f).AddInput(0.01f);
            dataManager.Expecteds(1).AddExpected(0.02f);
            NeuronGenerator neuronGenerator = new NeuronGenerator();
            neuronGenerator.WeightConfiguration.SetMaxAndMin(1, 1);
            neuronGenerator.BiasConfiguration.SetMaxAndMin(1, 1);

            var nNGen = new NNGenerator(neuronGenerator);

            var neuralNetwork = nNGen.GenerateDefault(layers);
            var trainer = new Trainer(dataManager, neuralNetwork, new FeedForward(), new Backpropagation(), new FileTraceLogService());

            trainer.Fit(epochs);

            Console.WriteLine($"\nTraining Time (my code): {trainer.TimeOfTraining} ms");

            return neuralNetwork;
        }

        private static void Fit(ExampleNeuralNetwork exampleNeuralNetwork, ITraceLog traceLog)
        {
            exampleNeuralNetwork.FeedForward(new float[] { 0.01f, 0.01f });
            exampleNeuralNetwork.BackPropagate(new float[] { 0.01f, 0.01f }, new float[] { 0.02f });
            traceLog.Log(exampleNeuralNetwork.ToString());
        }

        private static void PrintResult(NeuralNetwork neuralNetwork, ExampleNeuralNetwork exampleNeuralNetwork)
        {
            Console.WriteLine("-------------");
            Console.WriteLine(neuralNetwork);
            Console.WriteLine("- ex:");
            Console.WriteLine(exampleNeuralNetwork.ToString());
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

        private static List<float> GenerateY(int totalOfIteractions)
        {
            var yValues = new List<float>();

            for (float i = 0; i < totalOfIteractions; i++)
            {
                float input = i / totalOfIteractions;
                yValues.Add(input);
            }

            return yValues;
        }


        private static List<float> Predict(INeuralNetwork neuralNetwork, int totalOfIterations)
        {
            var listResult = new List<float>();

            for (float i = 0; i < totalOfIterations; i++)
            {
                float input = i / totalOfIterations;
                var inputManager = new InputInserter(2);
                inputManager.AddInput(input);
                inputManager.AddInput(input);

                var result = neuralNetwork.Predict(inputManager.Get());
                listResult.Add(result[0]);
            }

            return listResult;
        }

        private static List<float> GenerateExpectedData(int totalOfIteractions)
        {
            var expectedResults = new List<float>();

            for(float i = 0; i < totalOfIteractions; i++)
            {
                float input = i / totalOfIteractions;
                expectedResults.Add(input * 2);
            }

            return expectedResults;
        }

        private static void PlotChart(List<float> listResult, List<float> listOfExpectedResults, List<float> yValues)
        {
            float[] xValues = listResult.ToArray();
            float[] xValues2 = listOfExpectedResults.ToArray();
            
            var chart = GenerateChart(xValues, yValues.ToArray(), "results");
            var chart2 = GenerateChart(xValues2, yValues.ToArray(), "expected");
            var chartList = new List<GenericChart.GenericChart>();

            chartList.Add(chart);
            chartList.Add(chart2);

            var combinedChart = Chart.Combine(chartList);

            combinedChart.Show();
        }

        private static GenericChart.GenericChart GenerateChart(float[] xValues, float[] yValues, string legend)
        {
            Plotly.NET.Trace trace;

            LinearAxis xAxis = new LinearAxis();
            xAxis.SetValue("title", "xAxis");
            xAxis.SetValue("showgrid", false);
            xAxis.SetValue("showline", true);

            LinearAxis yAxis = new LinearAxis();
            yAxis.SetValue("title", "yAxis");
            yAxis.SetValue("showgrid", false);
            yAxis.SetValue("showline", true);

            var layout = new Layout();
            layout.SetValue("xaxis", xAxis);
            layout.SetValue("yaxis", yAxis);
            layout.SetValue("showlegend", true);

            trace = new Plotly.NET.Trace("scatter");
            trace.SetValue("x", xValues);
            trace.SetValue("y", yValues);

            trace.SetValue("mode", "markers");
            trace.SetValue("name", legend);

            return GenericChart
                            .ofTraceObject(true, trace)
                            .WithLayout(layout);
        }
    }
}
