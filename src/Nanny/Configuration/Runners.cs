using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nanny.Configuration
{
    internal static class Runners
    {

        internal readonly static Func<Func<CancellationToken, Task>, NannyConfig, Task> ForeverRunner = (function, nannyConfig) =>
        {
            while (true)
            {
                function(nannyConfig.Cts.Token);
            }
        };

        internal readonly static Func<Func<CancellationToken, Task>, NannyConfig, Task> MultipleRunner = (function, nannyConfig) =>
        {
            for (int i = 0; i < nannyConfig.Retries; i++)
            {
                function(nannyConfig.Cts.Token);
            }
            return Task.CompletedTask;
        };


        internal readonly static Func<Func<CancellationToken, Task>, NannyConfig, Task> SingleRunner = (function, nannyConfig) => function(nannyConfig.Cts.Token);
    }
}
