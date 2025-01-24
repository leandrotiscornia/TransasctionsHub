namespace _011Global.Shared;

public class WorkerCancellationToken : CancellationTokenBase
{
    private readonly CancellationToken _cancellationToken;
    public WorkerCancellationToken(CancellationTokenSource cts)
    {
        _cancellationToken = cts.Token;
    }
    public override CancellationToken Token { get { return _cancellationToken; } }
}

