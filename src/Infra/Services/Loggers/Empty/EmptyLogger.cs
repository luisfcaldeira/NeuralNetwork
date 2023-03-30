using MyNeuralNetwork.Domain.Interfaces.Services.Loggers;

namespace Core.Infra.Services.Loggers.Empty
{
    public class EmptyLogger : ITraceLogger
    {
        public void Log(object message)
        {
        }
    }
}
