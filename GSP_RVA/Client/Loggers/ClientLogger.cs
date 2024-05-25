using Common.Enums;
using Common.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace Common.Loggers
{
    public class ClientLogger : ILogger
    {
        public static ObservableCollection<string> LogMessages { get; set; } = new ObservableCollection<string>();

        public void Log(LogTraceLevel level, string message)
        {
            string timestamp = DateTime.Now.ToString("[dd.MM.yyyy. HH:mm]");
            string logMessage = $"{timestamp} {level}: {message}";

            LogMessages.Add(logMessage);

            using (StreamWriter sw = new StreamWriter("log.txt", append: true))
            {
                sw.WriteLine(logMessage);
            }
        }
    }
}
