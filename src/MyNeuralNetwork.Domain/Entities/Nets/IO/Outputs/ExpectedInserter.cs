using MyNeuralNetwork.Domain.Entities.Support.ValueInserters;

namespace MyNeuralNetwork.Domain.Entities.Nets.IO.Outputs
{
    public class ExpectedInserter : Inserter<Expected>
    {
        public ExpectedInserter(int sizeOfInput) : base(sizeOfInput)
        {
        }

        public ExpectedInserter AddExpected(float value)
        {
            Add(new Expected(value));
            return this;
        }
    }
}
