using MyNeuralNetwork.Domain.Interfaces.Services.Loggers;
using System.Diagnostics;

namespace Core.Infra.Services.Loggers.OutputWindow
{
    public class DebugLogger : ITraceLogger
    {
        public void Log(object message)
        {
            Debug.WriteLine(message);
        }
    }
}
