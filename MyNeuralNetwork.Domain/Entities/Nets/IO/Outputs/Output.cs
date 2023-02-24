using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;

namespace MyNeuralNetwork.Domain.Entities.Nets.IO.Outputs
{
    public class Output
    {
        public float Value { get; set; }

        public Output(float value)
        {
            Value = value;
        }

        public static float operator -(Output output, Expected expected)
        {
            return output.Value - expected.Value;
        }
    }
}
