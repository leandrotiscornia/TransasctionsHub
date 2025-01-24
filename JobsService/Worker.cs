using _011Global.JobsService.JobInterfaces;
using _011Global.Shared;
using _011Global.Shared.JobsServiceDBContext.Interfaces;

namespace _011Global.JobsService;
public class Worker : IHostedService
{
    private readonly ILogger<Worker> _logger;
    CancellationTokenSource _JoBSericeTokenSource;
    private readonly IEnumerable<IJob> _jobs;
    private readonly IServiceProvider _serviceProvider;
    private readonly string _hostName;

    public Worker(ILogger<Worker> logger, CancellationTokenSource cts, IEnumerable<IJob> jobs, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _JoBSericeTokenSource = cts;
        _jobs = jobs;
        _serviceProvider = serviceProvider;
        _hostName = Environment.MachineName;
    }



    public async Task StartAsync(CancellationToken cancellationToken)
    {

        _logger.LogInformation($"host {Environment.MachineName} main worker started");


        await Task.Factory.StartNew(async () => await MonitorJobs(_JoBSericeTokenSource.Token));



    }

    public async Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation($"main worker stopping jobs {DateTime.Now}");
        _JoBSericeTokenSource.Cancel();

        foreach (var job in _jobs)
        {
            await job.Stop();
        }

        _logger.LogInformation($"main worker exitted {DateTime.Now}");

    } 

    public async Task MonitorJobs(CancellationToken cancellationToken)
    {
        Dictionary<string,GlobalJob> DBJobs;
        while(!cancellationToken.IsCancellationRequested)
        {
            try
            {

                using var scope = _serviceProvider.CreateAsyncScope();
                var _jobRepo = scope.ServiceProvider.GetRequiredService<IJobsServiceRepository>();

                DBJobs = await _jobRepo.GetJobs(_hostName);
                foreach (IJob job in _jobs)
                    if (DBJobs.ContainsKey(job.Name) && DBJobs[job.Name].IsRunning == true)
                         job.Start();
                    else
                         job.Stop();
            }
            catch (Exception ex)
            {

                _logger.LogCritical($"MonitorJobs exception: {ex.Message}");
            }


            await Task.Delay(5000);
        }

    }
}