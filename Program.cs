using Serilog;
using Serilog.Events;
using Serilog.Templates;
using Serilog.Formatting.Compact;
using Serilog.Context;

Console.WriteLine("Hello, World!");

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .WriteTo.Console(
        // outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {Level:u3}] {Message:lj}{NewLine}{Exception}"
        formatter: new ExpressionTemplate("{ { time: @t, level: if @l = 'Verbose' then undefined() else if @l = 'Error' then @l else 'Log', messageTemplate: @mt, message: @m, ..@p, @x } }\n\n")
    )
    .Filter.ByExcluding("object.random < 10 OR number >= 10")
    .WriteTo.File(
        path: "events-.log",
        rollingInterval: RollingInterval.Minute,
        retainedFileCountLimit: 5,
        fileSizeLimitBytes: 1024,
        rollOnFileSizeLimit: true,
        formatter: new CompactJsonFormatter()
    )
    .WriteTo.File(
        path: "errors.log",
        restrictedToMinimumLevel: LogEventLevel.Warning,
        formatter: new CompactJsonFormatter()
    )
    .Enrich.WithProperty("prop-always", "estou sempre aqui")
    .Enrich.WithEnvironmentName()
    .Enrich.WithEnvironmentUserName()
    .Enrich.WithMachineName()
    .Enrich.FromLogContext()
    .CreateLogger();

Log.Verbose("Informações muito detalhadas.");
Log.Debug("Informações de depuração.");
Log.Information("Informações gerais.");
Log.Warning("Não são erros, mas podem podem levar a problemas no futuro.");
Log.Error("Erros que impedem o funcionamento correto.");
Log.Fatal("Erros graves que fazem o aplicativo parar de funcionar.");

using (LogContext.PushProperty("prop-context1", "valor global1"))
using (LogContext.PushProperty("prop-context2", "valor global2")) {
    var random = new Random();
    var count = 0;
    while (!Console.KeyAvailable || Console.ReadKey().Key != ConsoleKey.Escape) {
        var obj = new { count = ++count, random = random.Next() % 20 };
        Log.Verbose("Objeto: {@object}", obj);
        Log.Debug("Aleatório: {number}", obj.random);
        if (random.Next() % 100 <= 30) {
            var contextLog = Log
                .ForContext<Exception>()
                .ForContext("prop-erro", "quando dá erro");
            contextLog.Error("Ocorreu um erro: {error}", new Exception("My Bad"));
        }
        Thread.Sleep(1000);
    }
}