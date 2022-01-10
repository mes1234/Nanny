using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Nanny.Configuration
{
    public class NannyConfig
    {
        public StartOptions StartOptions { get; init; }

        public CancellationTokenSource Cts { get; init; }
    }
}
