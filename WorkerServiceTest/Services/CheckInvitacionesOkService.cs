using SimpleCronWorkerService;

namespace WorkerServiceTest.Services
{
    public class CheckInvitacionesOkService : CronWorkerService
    {
        private readonly ILogger<CheckInvitacionesOkService> _logger;
        private HttpClient _client;
        public CheckInvitacionesOkService(CronWorkerServiceSettings<CheckInvitacionesOkService> settings, ILogger<CheckInvitacionesOkService> logger)
            : base(settings)
        {
            _logger = logger;
            _client= new HttpClient();
            _client.Timeout = TimeSpan.FromMinutes(1);
        }

        protected override async Task DoWork(CancellationToken cancellationToken)
        {
            try
            {
                using HttpResponseMessage response = await _client.GetAsync("https://invitacionesok.com");
                response.EnsureSuccessStatusCode();
                _logger.LogInformation("The site invitacionesOk response with success status code: {statusCode} at: {date}", response.StatusCode, DateTime.Now);
                
            }
            catch (HttpRequestException e)
            {
                _logger.LogError("\nException Caught!");
                _logger.LogError("Message :{0} ", e.Message);
            }
            await Task.CompletedTask;
        }
    }
}
