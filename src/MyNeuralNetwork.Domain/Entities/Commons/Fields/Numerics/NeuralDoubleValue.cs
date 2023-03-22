namespace MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics
{
    public class NeuralDoubleValue : NeuralValue<double>
    {
        public NeuralDoubleValue(double value) : base(value)
        {
        }

        public NeuralDoubleValue() : base(0)
        {
        }
    }
}
