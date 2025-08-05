// File: Services/ScrapingControlCenter.cs
using System;
using System.Threading;
using System.Threading.Tasks;

public static class AsyncTaskController
{
    public static CancellationTokenSource Cts { get; set; } = new CancellationTokenSource();
    public static ManualResetEventSlim PauseEvent { get; set; } = new ManualResetEventSlim(true);
    public static IProgress<string> ?ProgressReporter { get; set; }
}
