using MyNeuralNetwork.Domain.Interfaces.Services.Logs;
using System.IO;

namespace Core.Infra.Services.Loggers.Files
{
    public class FileTraceLogService : ITraceLog
    {
        public string FileName { get; } = "log.txt";

        public FileTraceLogService() { }

        public FileTraceLogService(string fileName) : this()
        {
            FileName = fileName;
        }

        public void Log(object message)
        {
            using StreamWriter file = File.AppendText(FileName);
            file.WriteLine(message.ToString());
        }
    }
}
