using _011Global.Shared;

namespace _011Global.JobsService.JobInterfaces;
public abstract class Job
    {
        protected abstract int IterationWaitTime { get; }
        private CancellationToken _globalCancellationToken { get; set;  }
        protected CancellationToken cancellationToken;
        protected CancellationTokenSource cts { get; set; }

        protected ILogger logger { get; private set;  }

        public string Name { get; private set; }

        private Task RunningTask { get; set; }
        public Job(CancellationTokenBase globalCancellationToken, ILogger logger)
        {
        this.
            _globalCancellationToken = globalCancellationToken;
            this.logger = logger;
            Name = this.GetType().FullName; 
            
        }

        protected abstract Task WorkLoad(CancellationToken cancellationToken);

    public async Task Start()
    {

        if (RunningTask != null && !RunningTask.IsCompleted)
            return;
        cts = CancellationTokenSource.CreateLinkedTokenSource(_globalCancellationToken);
        cancellationToken = cts.Token;
        logger.LogInformation($"{Name} started {DateTime.Now}");
        RunningTask = await Task.Factory.StartNew(async () =>
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    await WorkLoad(cancellationToken);
                    await Task.Delay(IterationWaitTime, cancellationToken);
                }
                catch (TaskCanceledException ex) { }
                catch (Exception ex)
                {
                    logger.LogCritical($"{Name} threw an exception: {ex.Message}");
                    //log major exception here
                }
            }
        });

        }

        public virtual async Task Stop()
        {
            if (RunningTask != null && !RunningTask.IsCompleted)
            {
                cts.Cancel();
                await RunningTask;
                logger.LogInformation($"{Name} stopped {DateTime.Now}");
            }

        }

}

