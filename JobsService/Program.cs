using _011Global.JobsService.JobInterfaces;
using _011Global.JobsService;
using _011Global.JobsService.Services;
using _011Global.Shared;
using _011Global.Shared.JobsServiceDBContext.Interfaces;
using _011Global.Shared.JobsServiceDBContext.Repos;
using _011Global.Shared.USAEPayAPI;
using _011Global.Shared.USAEPayAPI.Interfaces;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((host, services) =>
    {

        services.AddSingleton<CancellationTokenSource>(_ => (new CancellationTokenSource()))
        .AddTransient<CancellationTokenBase, WorkerCancellationToken>()
        .LoadInterfacesSingleton<IJob>()
        .RegisterDBContexts(host.Configuration.GetConnectionString("TransactionsHubDB"))
        .AddHostedService<Worker>();

        services.AddSingleton<IUSAEpayAPIHelper, USAEpayAPIHelper>();
    })
    .UseSystemd()
    .ConfigureAppConfiguration(configBuilder=>
    {
        var env =  Environment.GetEnvironmentVariables()["DOTNET_ENVIRONMENT"];
        configBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile($"appsettings.{env}.json", true, true);
    })
    .Build();
 

host.Run();
