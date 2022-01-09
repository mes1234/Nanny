using Nanny;
using Playground;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddTransient<INanny, AsyncNanny>();
    })
    .Build();

await host.RunAsync();
