using Common.Enums;
using Common.Interfaces;
using MVVMLight.Messaging;
using NetworkService.Helpers;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace Common.Loggers
{
    public class ClientLogger : BindableBase, ILogger
    {
        private static readonly object lockObj = new object();
        private static readonly string logFilePath = "log.txt";

        public static ObservableCollection<string> LogMessages { get; } = new ObservableCollection<string>();

        public void Log(LogTraceLevel level, string message)
        {
            try
            {
                string timestamp = DateTime.Now.ToString("[dd.MM.yyyy. HH:mm]");
                string logMessage = $"{timestamp} {level}: {message}";

                // Add to the start of log
                lock (lockObj)
                {
                    LogMessages.Insert(0, logMessage);
                }

                // Broadcast log message
                Messenger.Default.Send(logMessage);

                // Write to log file
                WriteToFile(logMessage);
            }
            catch (Exception ex)
            {
                // Log any exceptions that occur during logging
                Console.WriteLine($"Error logging message: {ex.Message}");
            }
        }

        private void WriteToFile(string message)
        {
            try
            {
                lock (lockObj)
                {
                    using (StreamWriter sw = new StreamWriter(logFilePath, append: true))
                    {
                        sw.WriteLine(message);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any exceptions that occur during file writing
                Console.WriteLine($"Error writing to log file: {ex.Message}");
            }
        }
    }
}
