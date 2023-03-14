using Serilog;
using Serilog.Events;

Console.WriteLine("Hello, World!");

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .WriteTo.Console(
        outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {Level:u3}] {Message:lj}{NewLine}{Exception}"
    )
    .WriteTo.File(
        path: "events-.log",
        rollingInterval: RollingInterval.Minute,
        retainedFileCountLimit: 5,
        fileSizeLimitBytes: 1024,
        rollOnFileSizeLimit: true
    )
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

var random = new Random();
var count = 0;
while (!Console.KeyAvailable || Console.ReadKey().Key != ConsoleKey.Escape) {
    var obj = new { count = ++count, random = random.Next() };
    Log.Verbose("Objeto: {@object}", obj);
    Log.Debug("Aleatório: {number}", obj.random);
    Thread.Sleep(100);
}
