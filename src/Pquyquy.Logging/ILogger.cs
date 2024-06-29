using System.Reflection;

namespace Pquyquy.Logging
{
    /// <summary>
    /// Provides logging functionality with various severity levels.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs an informational message.
        /// </summary>
        /// <typeparam name="T">The type of the source of the log message.</typeparam>
        /// <param name="methodName">The name of the method that executed the logging.</param>
        /// <param name="message">The message to log.</param>
        void Info<T>(string methodName, string message);

        /// <summary>
        /// Logs a debug message.
        /// </summary>
        /// <typeparam name="T">The type of the source of the log message.</typeparam>
        /// <param name="methodName">The name of the method that executed the logging.</param>
        /// <param name="message">The message to log.</param>
        void Debug<T>(string methodName, string message);

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <typeparam name="T">The type of the source of the log message.</typeparam>
        /// <param name="methodName">The name of the method that executed the logging.</param>
        /// <param name="message">The message to log.</param>
        /// <param name="exception">The exception to log, if any.</param>
        void Error<T>(string methodName, string message, Exception exception = null);

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <typeparam name="T">The type of the source of the log message.</typeparam>
        /// <param name="methodName">The name of the method that executed the logging.</param>
        /// <param name="message">The message to log.</param>
        /// <param name="exception">The exception to log, if any.</param>
        void Warn<T>(string methodName, string message, Exception exception = null);

        /// <summary>
        /// Logs a fatal error message.
        /// </summary>
        /// <typeparam name="T">The type of the source of the log message.</typeparam>
        /// <param name="methodName">The name of the method that executed the logging.</param>
        /// <param name="message">The message to log.</param>
        /// <param name="exception">The exception to log, if any.</param>
        void Fatal<T>(string methodName, string message, Exception exception = null);

        /// <summary>
        /// Logs an informational message.
        /// </summary>
        /// <param name="methodBase">The method base representing the method that executed the logging.</param>
        /// <param name="message">The message to log.</param>
        void Info(MethodBase methodBase, string message);

        /// <summary>
        /// Logs a debug message.
        /// </summary>
        /// <param name="methodBase">The method base representing the method that executed the logging.</param>
        /// <param name="message">The message to log.</param>
        void Debug(MethodBase methodBase, string message);

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="methodBase">The method base representing the method that executed the logging.</param>
        /// <param name="message">The message to log.</param>
        /// <param name="exception">The exception to log, if any.</param>
        void Error(MethodBase methodBase, string message, Exception exception = null);

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="methodBase">The method base representing the method that executed the logging.</param>
        /// <param name="message">The message to log.</param>
        /// <param name="exception">The exception to log, if any.</param>
        void Warn(MethodBase methodBase, string message, Exception exception = null);

        /// <summary>
        /// Logs a fatal error message.
        /// </summary>
        /// <param name="methodBase">The method base representing the method that executed the logging.</param>
        /// <param name="message">The message to log.</param>
        /// <param name="exception">The exception to log, if any.</param>
        void Fatal(MethodBase methodBase, string message, Exception exception = null);
    }
}
