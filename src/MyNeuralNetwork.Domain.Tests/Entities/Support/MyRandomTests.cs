using MyNeuralNetwork.Domain.Entities.Support;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MyNeuralNetwork.Domain.Tests.Entities.Support
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
            var values = new List<double>();

            for (var i = 0; i < 1000; i++)
            {
                values.Add(MyRandom.Range(0, 1));
            }

            Assert.That(values.Average(), Is.AtLeast(0.47));
            Assert.That(values.Average(), Is.AtMost(0.53));

            TestContext.WriteLine($"Itens average: {values.Average()}.");
        }
    }
}
