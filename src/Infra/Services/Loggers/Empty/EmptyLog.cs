using MyNeuralNetwork.Domain.Interfaces.Services.Logs;

namespace Core.Infra.Services.Loggers.Empty
{
    public class EmptyLog : ITraceLog
    {
        public void Log(object message)
        {
        }
    }
}
