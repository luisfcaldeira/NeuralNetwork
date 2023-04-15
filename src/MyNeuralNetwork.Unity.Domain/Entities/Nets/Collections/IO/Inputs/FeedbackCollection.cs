using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using System.Collections.Generic;
using System.Linq;

namespace MyNeuralNetwork.Domain.Entities.Nets.Collections.IO.Inputs
{
    public class FeedbackCollection : List<Feedback>
    {
        internal double SumValues()
        {
            return this.Select(f => f.Value).Sum();
        }
    }
}
