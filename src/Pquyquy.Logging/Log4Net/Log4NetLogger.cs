using log4net;
using log4net.Config;
using System.Reflection;
using System.Xml;

namespace Pquyquy.Logging.Log4Net;

/// <summary>
/// Logger implementation using log4net for logging messages.
/// </summary>
public class Log4NetLogger : ILogger
{
    private readonly Func<string, bool>? _messageFilterFunc;

    /// <summary>
    /// Default constructor. Configures log4net using default configuration.
    /// </summary>
    public Log4NetLogger()
    {
        XmlConfigurator.Configure();
    }

    /// <summary>
    /// Constructor with XML configuration element and message filter function.
    /// Configures log4net using provided XML configuration.
    /// </summary>
    /// <param name="xmlElement">XML element containing log4net configuration.</param>
    /// <param name="messageFilterFunc">Function to filter messages.</param>
    public Log4NetLogger(XmlElement xmlElement, Func<string, bool>? messageFilterFunc)
    {
        _messageFilterFunc = messageFilterFunc;
        XmlConfigurator.Configure(xmlElement);
    }

    /// <inheritdoc/>
    public void Info<T>(string methodName, string message)
    {
        Log(LogManager.GetLogger(typeof(T)), LogLevel.Info, methodName, message);
    }

    /// <inheritdoc/>
    public void Debug<T>(string methodName, string message)
    {
        Log(LogManager.GetLogger(typeof(T)), LogLevel.Debug, methodName, message);
    }

    /// <inheritdoc/>
    public void Error<T>(string methodName, string message, Exception exception = null)
    {
        Log(LogManager.GetLogger(typeof(T)), LogLevel.Error, methodName, message, exception);
    }

    /// <inheritdoc/>
    public void Warn<T>(string methodName, string message, Exception exception = null)
    {
        Log(LogManager.GetLogger(typeof(T)), LogLevel.Warn, methodName, message, exception);
    }

    /// <inheritdoc/>
    public void Fatal<T>(string methodName, string message, Exception exception = null)
    {
        Log(LogManager.GetLogger(typeof(T)), LogLevel.Fatal, methodName, message, exception);
    }

    /// <inheritdoc/>
    public void Info(MethodBase methodBase, string message)
    {
        var T = GetDeclaringTypeFullName(methodBase);
        var methodName = GetMethodName(methodBase);
        Log(LogManager.GetLogger(T), LogLevel.Info, methodName, message);
    }

    /// <inheritdoc/>
    public void Debug(MethodBase methodBase, string message)
    {
        var T = GetDeclaringTypeFullName(methodBase);
        Log(LogManager.GetLogger(T), LogLevel.Debug, methodBase.Name, message);
    }

    /// <inheritdoc/>
    public void Error(MethodBase methodBase, string message, Exception exception = null)
    {
        var T = GetDeclaringTypeFullName(methodBase);
        Log(LogManager.GetLogger(T), LogLevel.Error, methodBase.Name, message, exception);
    }

    /// <inheritdoc/>
    public void Warn(MethodBase methodBase, string message, Exception exception = null)
    {
        var T = GetDeclaringTypeFullName(methodBase);
        Log(LogManager.GetLogger(T), LogLevel.Warn, methodBase.Name, message, exception);
    }

    /// <inheritdoc/>
    public void Fatal(MethodBase methodBase, string message, Exception exception = null)
    {
        var T = GetDeclaringTypeFullName(methodBase);
        Log(LogManager.GetLogger(T), LogLevel.Fatal, methodBase.Name, message, exception);
    }

    /// <summary>
    /// Logs a message at the specified log level using log4net.
    /// </summary>
    private void Log(ILog log, LogLevel logLevel, string methodName, string message, Exception exception = null)
    {
        if (ShouldLogMessage(message, exception))
        {
            ThreadContext.Properties["MethodName"] = !string.IsNullOrEmpty(methodName) ? methodName : "?";
            try
            {
                switch (logLevel)
                {
                    case LogLevel.Fatal when log.IsFatalEnabled:
                        log.Fatal(message, exception);
                        break;
                    case LogLevel.Debug when log.IsDebugEnabled:
                        log.Debug(message, exception);
                        break;
                    case LogLevel.Error when log.IsErrorEnabled:
                        log.Error(message, exception);
                        break;
                    case LogLevel.Info when log.IsInfoEnabled:
                        log.Info(message, exception);
                        break;
                    case LogLevel.Warn when log.IsWarnEnabled:
                        log.Warn(message, exception);
                        break;
                    default:
                        log.Warn($"Encountered unknown log level {logLevel}, writing out as Info.");
                        log.Info(message, exception);
                        break;
                }
            }
            finally
            {
                ThreadContext.Properties.Remove("MethodName");
            }
        }
    }

    /// <summary>
    /// Determines if the message should be logged based on filters and message contents.
    /// </summary>
    private bool ShouldLogMessage(string message, Exception? exception)
    {
        return !(_messageFilterFunc?.Invoke(message) == true || string.IsNullOrWhiteSpace(message) && exception == null);
    }

    public static Type GetDeclaringTypeFullName(MethodBase methodBase)
    {
        if (methodBase == null)
            throw new ArgumentNullException(nameof(methodBase), "MethodBase cannot be null.");

        return methodBase.DeclaringType.DeclaringType ?? throw new ArgumentException("MethodBase does not have a valid DeclaringType.DeclaringType.");
    }

    public static string GetMethodName(MethodBase methodBase)
    {
        if (methodBase == null)
            throw new ArgumentNullException(nameof(methodBase), "MethodBase cannot be null.");

        return methodBase.DeclaringType.Name ?? throw new ArgumentException("MethodBase does not have a valid DeclaringType.Name.");
    }
}