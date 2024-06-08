using Common.Enums;
using Common.Interfaces;
using System;
using System.IO;

namespace Service.Loggers
{
    /// <summary>
    /// Provides logging functionality to a file.
    /// </summary>
    public class FileLogger : ILogger
    {
        /// <summary>
        /// Logs a message to a file with the specified log level.
        /// </summary>
        /// <param name="level">The log level.</param>
        /// <param name="message">The message to log.</param>
        public void Log(LogTraceLevel level, string message)
        {
            string timestamp = DateTime.Now.ToString("[dd.MM.yyyy. HH:mm]");
            string logMessage = $"{timestamp} {level}: {message}";

            try
            {
                using (StreamWriter sw = new StreamWriter("log.txt", append: true))
                {
                    sw.WriteLine(logMessage);
                }
            }
            catch (Exception ex)
            {
                // If logging fails, write to console
                Console.WriteLine($"Error logging to file: {ex.Message}");
            }
        }
    }
}
