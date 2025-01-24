namespace _011Global.Shared;

public abstract class CancellationTokenBase
{
    public abstract CancellationToken Token { get; }

    public static implicit operator CancellationToken(CancellationTokenBase requestCancellation) =>
        requestCancellation.Token;
}