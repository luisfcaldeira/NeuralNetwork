using MyNeuralNetwork.Domain.Entities.Nets.Trainers.Genetics;
using MyNeuralNetwork.Domain.Interfaces.Networks;
using MyNeuralNetwork.Tests.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeuralNetwork.Domain.Tests.Entities.Nets.Trainers
{
    public class Trainers
    {
        [Test]
        public void TestIfItMutateTwoNetworks()
        {
            NetworkGeneratorForTests.FixedMinBias = 0.5;
            NetworkGeneratorForTests.FixedMaxBias = 0.5;
            NetworkGeneratorForTests.FixedMinWeight = 0.5;
            NetworkGeneratorForTests.FixedMaxWeight = 0.5;

            var trainer = new GeneticTrainer(1);

            var nets = new List<INeuralNetwork>()
            {
                NetworkGeneratorForTests.GiveMeOne(new int[] { 1, 1 }),
                NetworkGeneratorForTests.GiveMeOne(new int[] { 1, 1 }, false) 
            };
            nets[1].Fitness = 1;

            Assert.That(nets[1].Layers[0].Neurons[0].Bias, Is.EqualTo(0.5));
            Assert.That(nets[0].Layers[0].Neurons[0].Bias, Is.Not.EqualTo(0.5));

            trainer.Mutate(nets);

            Assert.That(nets[1].Layers[0].Neurons[0].Bias, Is.EqualTo(0.5));
            Assert.That(nets[0].Layers[0].Neurons[0].Bias, Is.EqualTo(0.5));

        }
    }
}
