using Nanny.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nanny
{
    /// <summary>
    /// <inheritdoc cref="INanny"/>
    /// </summary>
    public class AsyncNanny : INanny
    {
        private readonly NannyConfig _nannyConfig;
        private Func<Task>? _startFunction;

        public AsyncNanny(NannyConfig nannyConfig)
        {
            _nannyConfig = nannyConfig;
        }

        /// <inheritdoc />
        public INanny RegisterStart(Func<Task> func)
        {
            _startFunction = func;
            return this;
        }

        /// <inheritdoc />
        public INanny RegisterRestart(Func<Task> func)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task StartAsync()
        {
            _nannyConfig.Cts.Token.ThrowIfCancellationRequested();

            if (_startFunction == null) throw new NotImplementedException();

            return _startFunction();
        }
    }
}
