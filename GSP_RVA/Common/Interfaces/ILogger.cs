using Common.Enums;

namespace Common.Interfaces
{
    public interface ILogger
    {
        void Log(LogTraceLevel level, string message);
    }
}
