using Nanny;

namespace Playground
{
    public class WorkerB : BackgroundService
    {
        private readonly ILogger<WorkerB> _logger;
        private readonly INanny _nanny;

        public WorkerB(
            ILogger<WorkerB> logger,
            INanny nanny)
        {
            _logger = logger;
            _nanny = nanny;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _nanny
                .RegisterStart(async (token) =>
                 {
                     var counter = 0;
                     while (!token.IsCancellationRequested)
                     {
                         _logger.LogInformation("Worker B running start {times} times", counter);
                         await Task.Delay(1000, stoppingToken);
                         counter++;
                         if (counter > 10)
                             return;
                     }
                 })
                   .StartAsync();

        }
    }
}