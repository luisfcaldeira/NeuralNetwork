using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.Trainers.Genetics;
using MyNeuralNetwork.Domain.Interfaces.Networks;
using MyNeuralNetwork.Domain.Interfaces.Trainers.Genetics;
using MyNeuralNetwork.Tests.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyNeuralNetwork.Domain.Tests.Entities.Nets.Trainers
{
    public  class GeneticTrainersTests
    {
        protected IGeneticTrainer trainer;

        public virtual void TestIfItMutateTwoNetworks()
        {
            NetworkGenerator.FixedMinBias = 0.5;
            NetworkGenerator.FixedMaxBias = 0.5;
            NetworkGenerator.FixedMinWeight = 0.5;
            NetworkGenerator.FixedMaxWeight = 0.5;

            var nets = new List<INeuralNetwork>()
            {
                NetworkGenerator.GiveMeOne(new int[] { 1, 1 }),
                NetworkGenerator.GiveMeOne(new int[] { 1, 1 }),
                NetworkGenerator.GiveMeOne(new int[] { 1, 1 }),
                NetworkGenerator.GiveMeOne(new int[] { 1, 1 }),
                NetworkGenerator.GiveMeOne(new int[] { 1, 1 }, false),
                NetworkGenerator.GiveMeOne(new int[] { 1, 1 }, false),
            };
            nets[0].Fitness = 1;
            nets[1].Fitness = 2;
            nets[2].Fitness = 3;
            nets[3].Fitness = 4;

            Assert.Multiple(() =>
            {
                Assert.That(nets[4].Layers[0].Neurons[0].Bias, Is.EqualTo(0.5));
                Assert.That(nets[0].Layers[0].Neurons[0].Bias, Is.Not.EqualTo(0.5));
                Assert.That(nets[1].Layers[0].Neurons[0].Bias, Is.Not.EqualTo(0.5));
                Assert.That(nets[2].Layers[0].Neurons[0].Bias, Is.Not.EqualTo(0.5));
                Assert.That(nets[3].Layers[0].Neurons[0].Bias, Is.Not.EqualTo(0.5));
            });

            trainer.ToMutate(nets);

            Assert.Multiple(() =>
            {
                Assert.That(nets[4].Layers[0].Neurons[0].Bias, Is.EqualTo(0.5));
                Assert.That(nets[0].Layers[0].Neurons[0].Bias, Is.EqualTo(0.5));
                Assert.That(nets[1].Layers[0].Neurons[0].Bias, Is.EqualTo(0.5));
                Assert.That(nets[2].Layers[0].Neurons[0].Bias, Is.EqualTo(0.5));
                Assert.That(nets[3].Layers[0].Neurons[0].Bias, Is.EqualTo(0.5));
            });
        }

        public virtual void TestNeuralNetworkUse()
        {
            double expectedResult = 3;
            double quantity = 1000;
            double stdDev = (1 - 0) / Math.Sqrt(12);
            double expectedMean = 0.9;
            int epochs = 1000;

            var nets = new List<INeuralNetwork>();

            for (int i = 0; i < quantity; i++)
            {
                nets.Add(NetworkGenerator.GiveMeOne(new int[] { 2, 1 }));
            }

            foreach (var network in nets)
            {
                var result = network.Predict(new Input[] { new Input(1), new Input(2) });
                network.Fitness = Math.Abs(result[0] - expectedResult);
            }

            var trainer = new RouletteTrainer(new Mutater(expectedMean, -1, 1));

            trainer.ToMutate(nets);

            double sumOfMutateds = nets.Select(n => n.CounterOfMutations).Sum();
            double perc = sumOfMutateds / quantity;

            TestContext.WriteLine($"% mutated networks: {perc}");

            Assert.That(perc, Is.GreaterThan(expectedMean - stdDev));
            Assert.That(perc, Is.LessThan(expectedMean + stdDev));

            for (var i = 0; i < epochs; i++)
            {
                foreach (var network in nets)
                {
                    var result = network.Predict(new Input[] { new Input(1), new Input(2) });
                    network.Fitness = Math.Abs(result[0] - expectedResult);
                }

                trainer.ToMutate(nets);
            }

            var bestNet = trainer.GetTheBestOne(nets);

            var prediction = bestNet.Predict(new Input[] { new Input(1), new Input(2) });

            TestContext.WriteLine($"% Prediction: {prediction[0]}");

            Assert.That(prediction[0], Is.AtLeast(2.8));
            Assert.That(prediction[0], Is.AtMost(3.2));
        }

        public virtual void TestXor()
        {
            List<INeuralNetwork> nets = new List<INeuralNetwork>();

            GenerateAndIncludeNets(nets);

            Train(nets);

            var theBestNet = trainer.GetTheBestOne(nets);

            TryResults(theBestNet);
            AssertTest(theBestNet);
        }

        private static void GenerateAndIncludeNets(List<INeuralNetwork> nets)
        {
            for (var i = 0; i < 25; i++)
            {
                nets.Add(NetworkGenerator.GiveMeOneSigmoid(new int[] { 2, 2, 1 }));
            }
        }

        private void Train(List<INeuralNetwork> nets)
        {
            RouletteTrainer geneticTrainer = new RouletteTrainer(new Mutater());

            var dataManager = DataManagerGenerator.ForXor();
            double maxFitness = 1;
            int epochsCounter = 0;

            while (maxFitness > 0.01 && epochsCounter < 11000)
            {
                foreach (var net in nets)
                {
                    var inputs = dataManager.GetInputInserters();
                    var expecteds = dataManager.GetExpectedResults();
                    double fitness = 0;

                    for (var j = 0; j < inputs.Count; j++)
                    {
                        double result = net.Predict(inputs[j].Get())[0];
                        double expected = expecteds[j].Get()[0].Value;

                        fitness += Math.Pow(expected - result, 2);
                    }

                    net.Fitness = fitness;
                }

                epochsCounter++;
                geneticTrainer.ToMutate(nets);
                maxFitness = trainer.GetTheBestOne(nets).Fitness;
            }
            TestContext.WriteLine($"Epochs {epochsCounter}.");
        }

        private static void AssertTest(INeuralNetwork theBestNet)
        {
            var xor11 = theBestNet.Predict(new Input[] { new Input(1), new Input(1) })[0];
            var xor01 = theBestNet.Predict(new Input[] { new Input(0), new Input(1) })[0];
            var xor10 = theBestNet.Predict(new Input[] { new Input(1), new Input(0) })[0];
            var xor00 = theBestNet.Predict(new Input[] { new Input(0), new Input(0) })[0];
            Assert.Multiple(() =>
            {
                Assert.That(Math.Round(xor11, 0), Is.EqualTo(0));
                Assert.That(Math.Round(xor01, 0), Is.EqualTo(1));
                Assert.That(Math.Round(xor10, 0), Is.EqualTo(1));
                Assert.That(Math.Round(xor00, 0), Is.EqualTo(0));
            });
        }

        private static void TryResults(INeuralNetwork theBestNet)
        {
            var xor11 = theBestNet.Predict(new Input[] { new Input(1), new Input(1) })[0];
            var xor01 = theBestNet.Predict(new Input[] { new Input(0), new Input(1) })[0];
            var xor10 = theBestNet.Predict(new Input[] { new Input(1), new Input(0) })[0];
            var xor00 = theBestNet.Predict(new Input[] { new Input(0), new Input(0) })[0];

            TestContext.WriteLine($"true xor true: {Convert.ToBoolean(Math.Round(xor11, 0))} ({xor11})");
            TestContext.WriteLine($"false xor true: {Convert.ToBoolean(Math.Round(xor01, 0))} ({xor01})");
            TestContext.WriteLine($"true xor false: {Convert.ToBoolean(Math.Round(xor10, 0))} ({xor10})");
            TestContext.WriteLine($"false xor false: {Convert.ToBoolean(Math.Round(xor00, 0))} ({xor00})");
        }

        public virtual void TestChangeRuleTenPercent()
        {
            var mutater = new Mutater(0.1, 0, 0);
            double count = 0;

            double tries = 1000;

            for (int i = 0; i < tries; i++)
            {
                if (PassChance(mutater))
                    count++;
            }

            double result = count / tries;

            TestContext.WriteLine($"% of passes: {result}");

            Assert.That(result, Is.GreaterThan(0.05));
            Assert.That(result, Is.LessThan(0.15));
        }

        public virtual void TestChangeRuleFifthPercent()
        {
            var mutater = new Mutater(0.5, 0, 0);
            double count = 0;

            double tries = 1000;

            for (int i = 0; i < tries; i++)
            {
                if (PassChance(mutater))
                    count++;
            }

            double result = count / tries;

            TestContext.WriteLine($"% of passes: {result}");

            Assert.That(result, Is.GreaterThan(0.45));
            Assert.That(result, Is.LessThan(0.55));
        }

        private static bool PassChance(Mutater mutater)
        {
            var method = mutater.GetType().GetMethod("PassChance", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            return (bool)method.Invoke(mutater, Array.Empty<object>());
        }
    }
}
