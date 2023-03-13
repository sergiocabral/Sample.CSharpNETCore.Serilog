using Serilog;

Console.WriteLine("Hello, World!");

var loggerConfiguration = new LoggerConfiguration();
loggerConfiguration.WriteTo.Console();
var logger = loggerConfiguration.CreateLogger();

logger.Information("Hello, Logger!");