using System.Reflection;

namespace Pquyquy.Logging;

/// <summary>
/// Initializes the logger by finding and setting an implementation of the <see cref="ILogger"/> interface.
/// </summary>
public class LoggerBootstrapper
{
    /// <summary>
    /// Searches for an implementation of the <see cref="ILogger"/> interface within the loaded assemblies
    /// and sets it as the singleton instance for logging.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown when no implementation of <see cref="ILogger"/> is found, when multiple implementations are found,
    /// or when the implementation cannot be instantiated.
    /// </exception>
    public static void Initialize()
    {
        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? throw new InvalidOperationException("Failed to get executing assembly location.");
        var dllFiles = Directory.GetFiles(path, "*.dll");

        var implementingTypes = dllFiles
            .Where(dll => Path.GetFileName(dll).Contains("Pquyquy.Logging"))
            .SelectMany(dll => Assembly.LoadFrom(dll).GetExportedTypes())
            .Where(type => typeof(ILogger).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
            .ToList();

        if (!implementingTypes.Any())
        {
            throw new InvalidOperationException("No implementation of ILogger found.");
        }

        if (implementingTypes.Count > 1)
        {
            throw new InvalidOperationException("Multiple implementations of ILogger found.");
        }

        var implementation = implementingTypes.First();
        try
        {
            Logger.Instance = (ILogger)Activator.CreateInstance(implementation) ?? throw new InvalidOperationException($"Failed to create an instance of {implementation.FullName}.");
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to create an instance of {implementation.FullName}.", ex);
        }

        Logger.Instance.Info<LoggerBootstrapper>("Initialize", $"Logger instance created successfully. {implementation.FullName} will be used.");
    }
}