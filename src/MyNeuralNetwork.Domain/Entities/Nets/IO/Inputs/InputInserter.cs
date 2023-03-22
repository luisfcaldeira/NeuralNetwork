using MyNeuralNetwork.Domain.Entities.Support.ValueInserters;

namespace MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs
{
    public class InputInserter : Inserter<Input>
    {

        public InputInserter(int sizeOfInput) : base(sizeOfInput) { }

        public InputInserter AddInput(double input)
        {
            Add(new Input(input));
            return this;
        }
    }
}
