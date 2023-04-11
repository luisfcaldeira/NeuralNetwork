using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Outputs;
using System.Collections.Generic;

namespace MyNeuralNetwork.Domain.Entities.Nets.IO.Managers
{
    public class DataManager
    {
        private List<InputInserter> _inputInserters = new List<InputInserter>();
        private List<ExpectedInserter> _expectedInserters = new List<ExpectedInserter>();
        private static InputInserter _inputInserter = null;
        private static ExpectedInserter _expectedInserter = null;

        public int InputCount()
        {
            return _inputInserters.Count;
        }

        public InputInserter Inputs(int numOfItems)
        {
            _inputInserter = new InputInserter(numOfItems);
            _inputInserters.Add(_inputInserter);
            return _inputInserter;
        }
        
        public ExpectedInserter Expecteds(int numOfItems)
        {
            _expectedInserter = new ExpectedInserter(numOfItems);
            _expectedInserters.Add(_expectedInserter);
            return _expectedInserter;
        }

        public List<InputInserter> GetInputInserters()
        {
            return _inputInserters;
        }

        public List<ExpectedInserter> GetExpectedResults()
        {
            return _expectedInserters;
        }
    }
}
