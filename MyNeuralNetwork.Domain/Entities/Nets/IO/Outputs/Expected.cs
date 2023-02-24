namespace MyNeuralNetwork.Domain.Entities.Nets.IO.Outputs
{
    public class Expected 
    {
        public float Value { get; set; }

        public Expected(float value) 
        {
            Value = value;
        }
    }
}
