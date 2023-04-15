using MyNeuralNetwork.Domain.Interfaces.Services.Loggers;

namespace MyNeuralNetwork.Domain.Entities.Support.Loggers
{
    internal class EmptyLogger : ITraceLogger
    {
        public void Log(object message)
        {
        }
    }
}
