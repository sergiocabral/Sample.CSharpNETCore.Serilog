using Serilog;

Console.WriteLine("Hello, World!");

var loggerConfiguration = new LoggerConfiguration();
loggerConfiguration.MinimumLevel.Verbose();
loggerConfiguration.WriteTo.Console();
Log.Logger = loggerConfiguration.CreateLogger();

Log.Verbose("Informações muito detalhadas.");
Log.Debug("Informações de depuração.");
Log.Information("Informações gerais.");
Log.Warning("Não são erros, mas podem podem levar a problemas no futuro.");
Log.Error("Erros que impedem o funcionamento correto.");
Log.Fatal("Erros graves que fazem o aplicativo parar de funcionar.");
