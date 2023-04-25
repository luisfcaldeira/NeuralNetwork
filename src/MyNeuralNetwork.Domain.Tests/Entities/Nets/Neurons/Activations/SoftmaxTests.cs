using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Activations;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeuralNetwork.Domain.Tests.Entities.Nets.Neurons.Activations
{
    public class SoftmaxTests
    {
        [Test]
        public void TestFormula()
        {
            double[] inputs = new double[] { 0.1d, 0.4d, 0.9d };

            var correctOutput = CalculateOutput(inputs);

            var entity = new Softmax();

            for(var i = 0; i < inputs.Length; i++)
            {
                TestContext.WriteLine(i + ": " + inputs[i]);
                var input = inputs[i];
                var result = entity.Activate(input);
                Assert.That(result, Is.EqualTo(correctOutput[i]));
            }
        }

        public double[] CalculateOutput(double[] inputs)
        {
            double[] outputs = new double[inputs.Length];
            double sum = 0.0;

            // Compute the exponentials of the inputs and their sum
            for (int i = 0; i < inputs.Length; i++)
            {
                outputs[i] = Math.Exp(inputs[i]);
                sum += outputs[i];
            }

            // Normalize the outputs by their sum
            for (int i = 0; i < outputs.Length; i++)
            {
                outputs[i] /= sum;
            }

            return outputs;
        }
    }
}
