using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Outputs;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Activations;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNeuralNetwork.Domain.Entities.Support;
using System.Diagnostics;

namespace MyNeuralNetwork.Domain.Tests.Support
{
    public class MyRandomTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestIfMyRandomGiveMeAMeanBetweenTwoValues()
        {
            var values = new List<float>();

            for(var i = 0; i < 1000; i++)
            {
                values.Add(MyRandom.Range(0, 1));
            }

            Assert.That(values.Average(), Is.AtLeast(0.48));
            Assert.That(values.Average(), Is.AtMost(0.52));

            TestContext.WriteLine($"Itens average: {values.Average()}.");
        }
    }
}
