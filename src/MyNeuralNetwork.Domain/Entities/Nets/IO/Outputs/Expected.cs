using MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics;

namespace MyNeuralNetwork.Domain.Entities.Nets.IO.Outputs
{
    public class Expected : FloatField
    {
        public Expected(float value) 
        {
            Value = value;
        }
    }
}
