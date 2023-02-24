using MyNeuralNetwork.Domain.Entities.Nets.IO.Outputs;

namespace MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs
{
    public class Feedback
    {
        public float Value { get; private set; }

        public Feedback(Output output, Expected expected)
        {
            Value = output - expected;
        }

        public Feedback(float gamma)
        {
            Value = gamma;
        }
    }
}
