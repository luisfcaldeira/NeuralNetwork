using MyNeuralNetwork.Domain.Interfaces.Services.Loggers;
using NUnit.Framework;

namespace MyNeuralNetwork.Domain.Tests.Entities.Support
{
    internal class TestLogger : ITraceLogger
    {
        private readonly int iteractions = 1;
        private int counter = 0;

        public TestLogger()
        {
            
        }

        public TestLogger(int iteractions)
        {
            this.iteractions = iteractions;
        }

        public void Log(object message)
        {
            counter++;
            if(counter % iteractions == 0)
                TestContext.WriteLine(message);
        }
    }
}
