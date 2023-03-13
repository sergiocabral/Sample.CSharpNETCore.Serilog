using Serilog;
using Serilog.Events;

Console.WriteLine("Hello, World!");

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .WriteTo.Console()
    .WriteTo.File("events.log")
    .WriteTo.File(
        path: "errors.log",
        restrictedToMinimumLevel: LogEventLevel.Warning
    )
    .CreateLogger();

Log.Verbose("Informações muito detalhadas.");
Log.Debug("Informações de depuração.");
Log.Information("Informações gerais.");
Log.Warning("Não são erros, mas podem podem levar a problemas no futuro.");
Log.Error("Erros que impedem o funcionamento correto.");
Log.Fatal("Erros graves que fazem o aplicativo parar de funcionar.");
