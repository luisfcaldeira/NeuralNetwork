using MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics;

namespace MyNeuralNetwork.Domain.Entities.Nets.IO.Outputs
{
    public class Output : FloatField
    {
        public Output(float value)
        {
            Value = value;
        }
    }
}
