using Serilog;

Console.WriteLine("Hello, World!");

var loggerConfiguration = new LoggerConfiguration();
loggerConfiguration.WriteTo.Console();
Log.Logger = loggerConfiguration.CreateLogger();

Log.Information("Hello, Logger!");
