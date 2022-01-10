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
        private readonly CancellationTokenSource _tokenSource;
        private Func<CancellationToken, Task>? _startFunction;
        private Func<CancellationToken, Task>? _restartFunction;

        public AsyncNanny(NannyConfig nannyConfig)
        {
            _nannyConfig = nannyConfig;
            _tokenSource = _nannyConfig.Cts;
        }

        /// <inheritdoc />
        public INanny RegisterStart(Func<CancellationToken, Task> func)
        {
            _startFunction = func;
            return this;
        }

        /// <inheritdoc />
        public INanny RegisterRestart(Func<CancellationToken, Task> func)
        {
            _restartFunction = func;
            return this;
        }

        /// <inheritdoc />
        public async Task StartAsync()
        {
            _tokenSource.Token.ThrowIfCancellationRequested();

            if (_startFunction == null) throw new NotImplementedException();
            if (_restartFunction == null) throw new NotImplementedException();

            await _startFunction(_tokenSource.Token).ConfigureAwait(false);

            await _restartFunction(_tokenSource.Token).ConfigureAwait(false);

            _tokenSource.Cancel();

        }
    }
}
