using Nanny;

namespace Playground
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly INanny _nanny;

        public Worker(
            ILogger<Worker> logger,
            INanny nanny)
        {
            _logger = logger;
            _nanny = nanny;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}