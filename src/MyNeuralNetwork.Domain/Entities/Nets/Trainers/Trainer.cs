using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Managers;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Outputs;
using MyNeuralNetwork.Domain.Entities.Nets.Networks;
using MyNeuralNetwork.Domain.Interfaces.Networks.Circuits.Backward;
using MyNeuralNetwork.Domain.Interfaces.Networks.Circuits.Forward;
using MyNeuralNetwork.Domain.Interfaces.Services.Loggers;
using System.Collections.Generic;
using System.Diagnostics;

namespace MyNeuralNetwork.Domain.Entities.Nets.Trainers
{
    public class Trainer 
    {
        private NeuralNetwork _neuralNetwork;
        private readonly ICircuitForward _circuitForward;
        private readonly ICircuitBackward _circuitBackward;
        private readonly ITraceLogger _traceLog;

        public DataManager DataManager { get; }
        public long TimeOfTraining { get; private set; }

        public Trainer(DataManager dataManager, NeuralNetwork neuralNetwork, ICircuitForward circuitForward, ICircuitBackward circuitBackward, ITraceLogger traceLog)
        {
            _neuralNetwork = neuralNetwork;
            _circuitForward = circuitForward;
            _circuitBackward = circuitBackward;
            _traceLog = traceLog;
            DataManager = dataManager;
        }


        public void Fit(int epochs)
        {
            var stopWatch = new Stopwatch();
            var inputInserters = DataManager.GetInputInserters();
            var inputExpecteds = DataManager.GetExpectedResults();

            stopWatch.Start();
            for (var epoch = 0; epoch < epochs; epoch++)
            {
                FitEpochs(inputInserters, inputExpecteds);
                _traceLog.Log($"epoch #{epoch}\n" + _neuralNetwork.ToString());
            }
            stopWatch.Stop();
            TimeOfTraining = stopWatch.ElapsedMilliseconds;
        }

        private void FitEpochs(List<InputInserter> inputInserters, List<ExpectedInserter> inputExpecteds)
        {
            for (var j = 0; j < inputInserters.Count; j++)
            {
                _circuitForward.Send(_neuralNetwork, inputInserters[j].Get());
                _circuitBackward.Send(_neuralNetwork, inputExpecteds[j].Get());
            }
        }
    }
}
