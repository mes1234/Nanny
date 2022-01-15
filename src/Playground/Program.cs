using Microsoft.Extensions.Logging.Abstractions;
using Nanny;
using Nanny.Configuration;
using Playground;

var cts = new CancellationTokenSource();

//cts.CancelAfter(10000);

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddTransient<INanny, AsyncNanny>();
        services.AddSingleton<NannyConfig>(sp => new NannyConfig
        {
            StartOptions = StartOptions.TryForever,
            ErrorHandler = ErrorHandlers.CatchLogContinue,
            Cts = cts,
            Retries = 2,
            Logger = sp.GetService<ILogger<NannyConfig>>() ?? NullLogger<NannyConfig>.Instance,
        });
    })
    .Build();

await host.RunAsync(cts.Token);
