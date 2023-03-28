using MyNeuralNetwork.Domain.Interfaces.Services.Logs;
using System.Diagnostics;

namespace Core.Infra.Services.Loggers.OutputWindow
{
    public class DebuggerLog : ITraceLog
    {
        public void Log(object message)
        {
            Debug.WriteLine(message);
        }
    }
}
