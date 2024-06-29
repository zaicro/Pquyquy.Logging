namespace Pquyquy.Logging;

/// <summary>
/// Provides a static accessor for a logger instance.
/// </summary>
public static class Logger
{
    private static ILogger instance;
    private static readonly object lockObject = new object();

    /// <summary>
    /// Gets or sets the singleton instance of the logger.
    /// </summary>
    /// <exception cref="TypeInitializationException">Thrown when trying to get an instance that has not been set.</exception>
    /// <exception cref="InvalidOperationException">Thrown when trying to set an instance that has already been set.</exception>
    public static ILogger Instance
    {
        get
        {
            if (instance == null)
            {
                throw new TypeInitializationException("Pquyquy.Logging.Logger", new Exception("No instance has been specified."));
            }
            return instance;
        }
        set
        {
            if (instance != null)
            {
                throw new InvalidOperationException("Instance has already been set.");
            }
            lock (lockObject)
            {
                instance ??= value ?? throw new ArgumentNullException(nameof(value), "Logger instance cannot be null.");
            }
        }
    }
}
