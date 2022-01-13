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
        });
    })
    .Build();

await host.RunAsync(cts.Token);
