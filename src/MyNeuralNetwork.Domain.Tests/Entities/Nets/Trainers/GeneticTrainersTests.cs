using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.Trainers.Genetics;
using MyNeuralNetwork.Domain.Entities.Support;
using MyNeuralNetwork.Domain.Interfaces.Networks;
using MyNeuralNetwork.Tests.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyNeuralNetwork.Domain.Tests.Entities.Nets.Trainers
{
    public class GeneticTrainersTests
    {

        
        [Test]
        public void TestIfItMutateTwoNetworks()
        {
            NetworkGeneratorForTests.FixedMinBias = 0.5;
            NetworkGeneratorForTests.FixedMaxBias = 0.5;
            NetworkGeneratorForTests.FixedMinWeight = 0.5;
            NetworkGeneratorForTests.FixedMaxWeight = 0.5;

            var trainer = new GeneticTrainer(new Mutater(1, 0, 0));

            var nets = new List<INeuralNetwork>()
            {
                NetworkGeneratorForTests.GiveMeOne(new int[] { 1, 1 }),
                NetworkGeneratorForTests.GiveMeOne(new int[] { 1, 1 }),
                NetworkGeneratorForTests.GiveMeOne(new int[] { 1, 1 }),
                NetworkGeneratorForTests.GiveMeOne(new int[] { 1, 1 }),
                NetworkGeneratorForTests.GiveMeOne(new int[] { 1, 1 }, false),
            };
            nets[0].Fitness = 1;
            nets[1].Fitness = 2;
            nets[2].Fitness = 3;
            nets[3].Fitness = 4;

            Assert.That(nets[4].Layers[0].Neurons[0].Bias, Is.EqualTo(0.5));
            Assert.That(nets[0].Layers[0].Neurons[0].Bias, Is.Not.EqualTo(0.5));
            Assert.That(nets[1].Layers[0].Neurons[0].Bias, Is.Not.EqualTo(0.5));
            Assert.That(nets[2].Layers[0].Neurons[0].Bias, Is.Not.EqualTo(0.5));
            Assert.That(nets[3].Layers[0].Neurons[0].Bias, Is.Not.EqualTo(0.5));

            trainer.Mutate(nets);

            Assert.That(nets[4].Layers[0].Neurons[0].Bias, Is.EqualTo(0.5));
            Assert.That(nets[0].Layers[0].Neurons[0].Bias, Is.EqualTo(0.5));
            Assert.That(nets[1].Layers[0].Neurons[0].Bias, Is.EqualTo(0.5));
            Assert.That(nets[2].Layers[0].Neurons[0].Bias, Is.EqualTo(0.5));
            Assert.That(nets[3].Layers[0].Neurons[0].Bias, Is.EqualTo(0.5));
        }

        [Test]
        public void TestNeuralNetworkUse()
        {
            double expectedResult = 3;
            double quantity = 1000;
            double stdDev = (1 - 0) / Math.Sqrt(12);
            double expectedMean = 0.5;
            int epochs = 1000;

            var nets = new List<INeuralNetwork>();

            for(int i = 0; i < quantity; i++) 
            {
                nets.Add(NetworkGeneratorForTests.GiveMeOne(new int[] { 2, 1 }));
            }

            foreach (var network in nets) 
            {
                var result = network.Predict(new Input[] { new Input(1),  new Input(2) });
                network.Fitness = Math.Abs(result[0] - expectedResult);
            }

            var trainer = new GeneticTrainer(new Mutater(expectedMean, 0, 0));

            trainer.Mutate(nets);

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

                trainer.Mutate(nets);
            }

            var bestNet = GeneticTrainer.GetTheBestOne(nets);

            var prediction = bestNet.Predict(new Input[] { new Input(1), new Input(2) });

            TestContext.WriteLine($"% Prediction: {prediction[0]}");

            Assert.That(prediction[0], Is.AtLeast(2.8));
            Assert.That(prediction[0], Is.AtMost(3.2));
        }

        [Test]
        public void TestChangeRuleTenPercent()
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

        [Test]
        public void TestChangeRuleFifthPercent()
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
