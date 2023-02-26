using MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Outputs;

namespace MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs
{
    public class Feedback : FloatField
    {
        
        public Feedback(Output output, Expected expected)
        {
            Value = output - expected;
        }
    }
}
