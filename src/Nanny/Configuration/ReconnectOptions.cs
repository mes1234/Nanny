using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nanny.Configuration
{
    public class StartOptions
    {
        public Func<Func<CancellationToken, Task>, NannyConfig, Task> Runner { get; init; }

        public StartOptions(Func<Func<CancellationToken, Task>, NannyConfig, Task> runner)
        {
            Runner = runner ?? throw new ArgumentNullException(nameof(runner));
        }

        public readonly static StartOptions TryForever = new StartOptions(Runners.ForeverRunner);
        public readonly static StartOptions TryMultiple = new StartOptions(Runners.MultipleRunner);
        public readonly static StartOptions TryOnce = new StartOptions(Runners.SingleRunner);

    }
}

