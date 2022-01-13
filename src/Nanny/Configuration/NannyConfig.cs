using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Nanny.Configuration
{
    public class NannyConfig
    {
        public StartOptions StartOptions { get; init; } = StartOptions.TryOnce;

        public CancellationTokenSource Cts { get; init; } = new CancellationTokenSource();

        public int Retries { get; init; } = 0;

        public ErrorHandlers ErrorHandler { get; init; } = ErrorHandlers.CatchLogContinue;
    }
}
