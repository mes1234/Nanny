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
            await _nanny
                .RegisterStart(async (token) =>
                 {
                     var counter = 0;
                     while (!token.IsCancellationRequested)
                     {
                         _logger.LogInformation("Worker running start {times} times", counter);
                         await Task.Delay(1000, stoppingToken);
                         counter++;
                         if (counter > 10) throw new Exception("Random one");
                         //return;
                     }
                     throw new Exception("Random one");
                 })
                .RegisterRestart(async (token) =>
                  {
                      var counter = 0;
                      while (!token.IsCancellationRequested)
                      {
                          _logger.LogInformation("Worker running restart {times} times", counter);
                          await Task.Delay(1000, stoppingToken);
                          counter++;
                          if (counter > 10) throw new Exception("Random one");
                          //return;
                      }
                      throw new Exception("Random one");
                  })
                   .StartAsync();

        }
    }
}