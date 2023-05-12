using SimpleCronWorkerService;

namespace SimpleCronWorkerServiceTest.Services
{
    public class Worker : CronWorkerService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(CronWorkerServiceSettings<Worker> cronWorkerServiceSettings, ILogger<Worker> logger)
            : base(cronWorkerServiceSettings)
        {
            _logger = logger;
        }

        protected override async Task DoWork(CancellationToken cancellationToken)
        {
            var guid = Guid.NewGuid();
            _logger.LogInformation("Worker1 at {date} - Task:{guid} -----> Runing", DateTime.UtcNow, guid);
            await Task.Delay(5000);
            _logger.LogInformation("Worker1 at {date} - Task:{guid} -----> Finished", DateTime.UtcNow, guid);
        }
    }
}
