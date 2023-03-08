using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Managers;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Outputs;
using MyNeuralNetwork.Domain.Entities.Nets.Networks;
using MyNeuralNetwork.Domain.Interfaces.Services.Logs;
using System.Collections.Generic;
using System.Diagnostics;

namespace MyNeuralNetwork.Domain.Entities.Nets.Trainers
{
    public class Trainer
    {
        private NeuralNetwork _neuralNetwork;
        private readonly ITraceLog _traceLog;

        public DataManager DataManager { get; }
        public long TimeOfTraining { get; private set; }

        public Trainer(DataManager dataManager, NeuralNetwork neuralNetwork, ITraceLog traceLog)
        {
            _neuralNetwork = neuralNetwork;
            _traceLog = traceLog;
            DataManager = dataManager;
        }


        public void Fit(int epochs)
        {
            var stopWatch = new Stopwatch();
            var inputInserters = DataManager.GetInputInserters();
            var inputExpecteds = DataManager.GetExpectedInserters();

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
                _neuralNetwork.Fit(inputInserters[j].Get(), inputExpecteds[j].Get());
                
            }
        }

    }
}
