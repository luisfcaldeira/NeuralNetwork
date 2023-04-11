using MyNeuralNetwork.Domain.Entities.Nets.IO.Managers;

namespace MyNeuralNetwork.Tests.Utils
{
    public static class DataManagerGenerator
    {
        public static DataManager ForXor()
        {
            var dataManager = new DataManager();

            dataManager.Inputs(2)
                .AddInput(1)
                .AddInput(1);
            dataManager.Expecteds(1).AddExpected(0);

            dataManager.Inputs(2)
                .AddInput(0)
                .AddInput(1);
            dataManager.Expecteds(1).AddExpected(1);

            dataManager.Inputs(2)
                .AddInput(1)
                .AddInput(0);
            dataManager.Expecteds(1).AddExpected(1);

            dataManager.Inputs(2)
                .AddInput(0)
                .AddInput(0);
            dataManager.Expecteds(1).AddExpected(0);

            return dataManager;
        }
    }
}
