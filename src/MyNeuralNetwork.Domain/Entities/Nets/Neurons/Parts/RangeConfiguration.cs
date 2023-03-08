using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts
{
    public class RangeConfiguration
    {
        public float MinimumRange { get; set; } = -0.5f;
        public float MaximumRange { get; set; } = 0.5f;

        public RangeConfiguration()
        {
            
        }

        public RangeConfiguration(float minimumRange, float maximumRange)
        {
            MinimumRange = minimumRange;
            MaximumRange = maximumRange;
        }

        public void SetMaxAndMin(float newMin, float newMax)
        {
            MinimumRange = newMin;
            MaximumRange = newMax;
        }

    }
}
