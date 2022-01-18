using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Nanny.Configuration
{
    public class NannyConfig
    {
        public StartOptions StartOptions { get; init; } = StartOptions.TryOnce;

        public static CancellationTokenSource Cts { get; set; } = new CancellationTokenSource();

        public int Retries { get; init; } = 0;

        public ErrorOptions ErrorOptions { get; init; } = ErrorOptions.CatchLogContinue;

        public ILogger Logger { get; init; } = NullLogger.Instance;
    }
}
