using SimpleCronWorkerService;
using WorkerServiceTest.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();

        services.AddCronWorkerService<CheckInvitacionesOkService>(options =>
        {
            // Run every 5 minutes
            options.CronExpression = @"*/10 * * * *";
            options.TimeZone = TimeZoneInfo.Local;
        });
    })
    .Build();

host.Run();
