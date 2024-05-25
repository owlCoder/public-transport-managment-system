using Common.Enums;
using Common.Interfaces;
using System.Collections.ObjectModel;

namespace Common.Loggers
{
    public class InMemoryLogger : ILogger
    {
        public static ObservableCollection<string> LogMessages { get; set; } = new ObservableCollection<string>();

        public void Log(LogTraceLevel level, string message)
        {
            // log u tj dodas u listu
        }
    }
}
