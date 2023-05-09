using System;

namespace HangfireTest.Services
{
    public class Worker
    {
        private readonly ILogger<Worker> _logger;
        private readonly SemaphoreSlim _semaphoreSlim;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            _semaphoreSlim = new SemaphoreSlim(1);
        }

        public async Task ExecuteAsync()
        {
            if (!await _semaphoreSlim.WaitAsync(TimeSpan.FromSeconds(10)))
            {
                _logger.LogInformation("Previous instance has not yet finish, skipping");
                return;
            }
            var guid = Guid.NewGuid();

            _logger.LogInformation("Running Worker1 Task:{0}... at {1}", guid, DateTime.UtcNow);
            await Task.Delay(20000);
            
            _semaphoreSlim.Release();
            _logger.LogInformation("Finished Worker1 Task:{0}... at {1}", guid, DateTime.UtcNow);
        }
    }
}
