using Microsoft.Extensions.Logging.Abstractions;
using Nanny;
using Nanny.Configuration;
using Playground;

NannyConfig.Cts = new CancellationTokenSource();

//cts.CancelAfter(10000);

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<WorkerA>();
        services.AddHostedService<WorkerB>();
        services.AddTransient<INanny, AsyncNanny>();
        services.AddSingleton<NannyConfig>(sp => new NannyConfig
        {
            StartOptions = StartOptions.TryForever,
            ErrorOptions = ErrorOptions.ThrowAndLog,
            Retries = 2,
            Logger = sp.GetService<ILogger<NannyConfig>>() ?? NullLogger<NannyConfig>.Instance,
        });
    })
    .Build();

await host.RunAsync(NannyConfig.Cts.Token);
