using MyNeuralNetwork.Domain.Entities.Nets.Generators;
using MyNeuralNetwork.Domain.Entities.Nets.Generators.Supports;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Activations;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts;
using MyNeuralNetwork.Domain.Entities.Nets.Trainers.Genetics;
using MyNeuralNetwork.Domain.Entities.Support;
using MyNeuralNetwork.Domain.Interfaces.Networks;
using MyNeuralNetwork.Domain.Interfaces.Neurons.Activations;

namespace MyNeuralNetwork.Tests.UseCases
{
    public class Tests
    {
        private RouletteTrainer geneticTrainer;

        [SetUp]
        public void Setup()
        {
            geneticTrainer = new RouletteTrainer(new Mutater(0.4, -0.2, 0.2));
        }

        [Test]
        public void TestIfGivePostiveValueForTwoInputs()
        {
            INeuralNetwork[] neuralNetworks = new INeuralNetwork[20];
            
            var epochs = 0;
            var bestFitness = double.MaxValue;
            for (var i = 0; i < neuralNetworks.Length; i++)
            {
                neuralNetworks[i] = GenterateNN();
            }
            
            while (epochs < 5000 //  && bestFitness > 0.1
                ) 
            {
                for (var i = 0; i < neuralNetworks.Length; i++)
                {
                    double fitness = 0;

                    const int tries = 20;

                    for (var j = 0; j < tries; j++)
                    {
                        Input xPosition = new Input(MyRandom.Range(-14, 14) / 14);
                        Input enemyDistance = new Input(MyRandom.Range(-5, 5) / 5);

                        if(enemyDistance.Value <= 0)
                        {
                            enemyDistance = new Input(0);
                        }

                        var result = neuralNetworks[i].Predict(new Input[] { enemyDistance, xPosition })[0];

                        if(enemyDistance.Value == 0)
                        {
                            fitness += Math.Pow(result, 2);
                        }
                        else
                        {
                            if(xPosition.Value < 0)
                            {
                                fitness += Math.Pow(1 - result, 2);
                            }
                            else
                            {
                                fitness += Math.Pow(1 - result, 2) * -1;
                            }
                        }
                    }
                    neuralNetworks[i].Fitness = fitness;
                }

                geneticTrainer.ToMutate(neuralNetworks);
                bestFitness = geneticTrainer.GetTheBestOne(neuralNetworks).Fitness;
                if(epochs % 500 == 0)
                {
                    TestContext.WriteLine($"best fitness: {bestFitness}");
                }
                epochs++;
            }

            var theBestNet = geneticTrainer.GetTheBestOne(neuralNetworks);
            TestContext.WriteLine($"Epochs: {epochs} | Best fitnes: {theBestNet.Fitness}");

            //Assert.That(theBestNet.Fitness, Is.LessThan(1));
            for (var i = 0; i < 10; i++)
            {
                var xPosition = new Input(MyRandom.Range(-14, 14) / 14);
                var enemyDistance = new Input(MyRandom.Range(-5, 5) / 5);

                if (enemyDistance.Value < 0)
                {
                    enemyDistance = new Input(0);
                }

                var actual = theBestNet.Predict(new Input[] { enemyDistance, xPosition })[0];

                TestContext.WriteLine($"{xPosition.Value * 14} & {enemyDistance.Value * 5}: {actual}");

            }
        }

        private static INeuralNetwork GenterateNN()
        {
            var neuronGen = new NeuronGenerator();
            var nngen = new NNGenerator(neuronGen, new LayersLinker());

            return nngen.Generate<SynapseManager>(new int[] { 2, 20, 1 }, new IActivator[] { new Tanh(), new Tanh(), new Tanh() });
        }

    }
}