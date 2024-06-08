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
        public static ObservableCollection<string> logMessages = new ObservableCollection<string>();

        public void Log(LogTraceLevel level, string message)
        {
            string timestamp = DateTime.Now.ToString("[dd.MM.yyyy. HH:mm]");
            string logMessage = $"{timestamp} {level}: {message}";

            // Add to the start of log
            logMessages.Insert(0, logMessage);

            // Dodavanje u logg na UI
            Messenger.Default.Send(logMessage);

            using (StreamWriter sw = new StreamWriter("log.txt", append: true))
            {
                sw.WriteLine(logMessage);
            }
        }
    }
}
