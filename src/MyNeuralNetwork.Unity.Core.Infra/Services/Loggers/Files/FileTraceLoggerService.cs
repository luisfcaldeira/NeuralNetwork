using MyNeuralNetwork.Domain.Interfaces.Services.Loggers;
using System.IO;

namespace Core.Infra.Services.Loggers.Files
{
    public class FileTraceLoggerService : ITraceLogger
    {
        public string FileName { get; } = "log.txt";

        public FileTraceLoggerService() { }

        public FileTraceLoggerService(string fileName) : this()
        {
            FileName = fileName;
        }

        public void Log(object message)
        {
            using (StreamWriter file = File.AppendText(FileName))
            {
                file.WriteLine(message.ToString());
            }
        }
    }
}
