using MyNeuralNetwork.Domain.Entities.Nets.Trainers.Genetics;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeuralNetwork.Domain.Tests.Entities.Nets.Trainers
{
    public class SimpleTrainerTests : GeneticTrainersTests
    {
        [SetUp]
        public void SetUp()
        {
            trainer = new SimpleTrainer(new Mutater(1, 0, 0));
        }

        [Test]
        public override void TestIfItMutateTwoNetworks()
        {
            base.TestIfItMutateTwoNetworks();
        }

        [Test]
        public override void TestNeuralNetworkUse()
        {
            base.TestNeuralNetworkUse();
        }

        [Test]
        public override void TestXor()
        {
            base.TestXor();
        }
        [Test]
        public override void TestChangeRuleTenPercent()
        {
            base.TestChangeRuleTenPercent();
        }

        [Test]
        public override void TestChangeRuleFifthPercent()
        {
            base.TestChangeRuleFifthPercent();
        }
    }
}
