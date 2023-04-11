namespace MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts
{
    public class RangeConfiguration
    {
        public double MinimumRange { get; set; } = -1f;
        public double MaximumRange { get; set; } = 1f;

        public RangeConfiguration()
        {
            
        }

        public RangeConfiguration(float minimumRange, float maximumRange)
        {
            MinimumRange = minimumRange;
            MaximumRange = maximumRange;
        }

        public void SetMaxAndMin(double newMin, double newMax)
        {
            MinimumRange = newMin;
            MaximumRange = newMax;
        }

    }
}
