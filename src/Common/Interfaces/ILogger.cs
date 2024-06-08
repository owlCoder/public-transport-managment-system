using Common.Enums;

namespace Common.Interfaces
{
    /// <summary>
    /// Interface for logging messages with specified trace levels.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs a message with the specified trace level.
        /// </summary>
        /// <param name="level">The trace level of the message.</param>
        /// <param name="message">The message to log.</param>
        void Log(LogTraceLevel level, string message);
    }
}
