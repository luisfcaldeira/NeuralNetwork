using MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics;

namespace MyNeuralNetwork.Domain.Entities.Nets.IO.Outputs
{
    public class Output : FloatNeuralValue
    {
        public Output()  : base(0)
        {
        }

        public Output(float value) : base(value)
        {
        }
    }
}
