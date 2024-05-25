using Common.Enums;
using Common.Interfaces;
using System.Collections.ObjectModel;

namespace Common.Loggers
{
    public class ClientLogger : ILogger
    {
        public static ObservableCollection<string> LogMessages { get; set; } = new ObservableCollection<string>();

        public void Log(LogTraceLevel level, string message)
        {
            // upisi u txt

            // log u tj dodas u listu
        }
    }
}
