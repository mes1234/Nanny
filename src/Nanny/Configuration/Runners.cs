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

        internal readonly static Func<Func<CancellationToken, Task>, NannyConfig, Task> ForeverRunner = async (function, nannyConfig) =>
        {
            while (true)
            {
                NannyConfig.Cts.Token.ThrowIfCancellationRequested();

                await function(NannyConfig.Cts.Token).ConfigureAwait(false);
            }
        };

        internal readonly static Func<Func<CancellationToken, Task>, NannyConfig, Task> MultipleRunner = async (function, nannyConfig) =>
        {
            for (int i = 0; i < nannyConfig.Retries; i++)
            {
                NannyConfig.Cts.Token.ThrowIfCancellationRequested();

                await function(NannyConfig.Cts.Token).ConfigureAwait(false);
            }
        };


        internal readonly static Func<Func<CancellationToken, Task>, NannyConfig, Task> SingleRunner = (function, nannyConfig) => function(NannyConfig.Cts.Token);
    }
}
