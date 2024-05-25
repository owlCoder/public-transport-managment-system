using Common.Enums;
using Common.Interfaces;
using System;
using System.IO;

namespace Service.Loggers
{
    public class FileLogger : ILogger
    {
        public void Log(LogTraceLevel level, string message)
        {
            string timestamp = DateTime.Now.ToString("[dd.MM.yyyy. HH:mm]");
            string logMessage = $"{timestamp} {level}: {message}";

            using (StreamWriter sw = new StreamWriter("log.txt", append: true))
            {
                sw.WriteLine(logMessage);
            }
        }
    }
}
