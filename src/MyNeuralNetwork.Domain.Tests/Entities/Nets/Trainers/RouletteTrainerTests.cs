using MyNeuralNetwork.Domain.Entities.Nets.Trainers.Genetics;
using NUnit.Framework;

namespace MyNeuralNetwork.Domain.Tests.Entities.Nets.Trainers
{
    public class RouletteTrainerTests : GeneticTrainersTests
    {
        [SetUp]
        public void SetUp()
        {
            trainer = new RouletteTrainer(new Mutater(1, 0, 0));
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
