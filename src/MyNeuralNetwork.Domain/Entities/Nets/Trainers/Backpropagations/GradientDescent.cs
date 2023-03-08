using MyNeuralNetwork.Domain.Entities.Nets.Interfaces.Trainers.Backpropagations;
using MyNeuralNetwork.Domain.Entities.Nets.Networks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeuralNetwork.Domain.Entities.Nets.Trainers.Backpropagations
{
    public class GradientDescent : IBackpropagation
    {
        public void Propagate(NeuralNetwork network)
        {
            throw new NotImplementedException();
        }
    }
}
