namespace MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics
{
    public class NeuralFloatValue : NeuralValue<float>
    {
        public NeuralFloatValue(NeuralFloatValue floatNeuralValue) : base(floatNeuralValue.Value)
        {
        }

        public NeuralFloatValue(float value) : base(value)
        {
        }

        public NeuralFloatValue() : this(0) { }

    }
}
