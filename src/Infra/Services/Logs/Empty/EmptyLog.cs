using MyNeuralNetwork.Domain.Interfaces.Services.Logs;

namespace Core.Infra.Services.Logs.Empty
{
    public class EmptyLog : ITraceLog
    {
        public void Log(object message)
        {
        }
    }
}
