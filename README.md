# Pquyquy.Logging

Pquyquy.Logging is a .NET library that forms part of the Pquyquy project group. This project utilizes Logger and log4net to enable comprehensive logging and tracking across various classes. It provides a versatile logging solution that can be easily integrated into any class requiring monitoring and diagnostics.

## Table of Contents

- [Installation](#installation)
- [Usage](#usage)
- [License](#license)

## Installation

Currently, the `Pquyquy.Logging` NuGet package is not available in the NuGet Gallery. You will need to download the project source code and generate the NuGet package locally. Follow these steps to get started:

   ```bash
   #1. Clone the repository or download the source code:
   git clone [URL]
   #2. Navigate to the project directory:
   cd Pquyquy.Logging
   #3. Restore dependencies and build the project
   dotnet restore
   dotnet build
   #4. Generate the NuGet package
   dotnet pack -c Release
   #5. The NuGet package (Pquyquy.Logging.[version].nupkg) will be generated in the bin/Release directory of the project. You can then reference this local package in your other projects as needed.
   ```

## Usage
  
   ```csharp
   Logger.Instance.Info<TClass>(nameof(MethodName), $"Message");
   Logger.Instance.Info(MethodBase.GetCurrentMethod(), $"Message");
   ```

Available Base Classes:
- Info: Logs an informational message.
- Debug: Logs a debug message.
- Error: Logs an error message.
- Warn: Logs a warning message.
- Fatal: Logs a fatal error message.
- Log: Logs a message at a specific log level.

## License

This project is licensed under the MIT License. 