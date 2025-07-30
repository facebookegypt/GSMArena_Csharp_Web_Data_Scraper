// File: Services/ScrapingControlCenter.cs
using System;
using System.Threading;
using System.Threading.Tasks;

public static class ScrapingControlCenter
{
    public static CancellationTokenSource Cts { get; private set; } = new CancellationTokenSource();

    public static ManualResetEventSlim PauseEvent { get; private set; } = new ManualResetEventSlim(true);

    public static Task ScrapeTask { get; set; }

    public static CancellationToken Token => Cts.Token;

    public static void Reset()
    {
        Cts?.Dispose();
        Cts = new CancellationTokenSource();

        PauseEvent?.Dispose();
        PauseEvent = new ManualResetEventSlim(true);
    }

    public static void Pause() => PauseEvent.Reset();
    public static void Resume() => PauseEvent.Set();
    public static void Cancel() => Cts.Cancel();
}
public static class AsyncTaskController
{
    public static CancellationTokenSource Cts { get; set; } = new CancellationTokenSource();
    public static ManualResetEventSlim PauseEvent { get; set; } = new ManualResetEventSlim(true);
    public static IProgress<string> ProgressReporter { get; set; }
}
