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
        private Func<CancellationToken, Task>? _startFunction;
        private Func<CancellationToken, Task>? _restartFunction;

        public AsyncNanny(NannyConfig nannyConfig)
        {
            _nannyConfig = nannyConfig ?? throw new ArgumentNullException(nameof(nannyConfig));
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

            try
            {
                _nannyConfig.Cts.Token.ThrowIfCancellationRequested();

                await _nannyConfig
                    .ErrorHandler
                    .Handle(StartFunctionRun());

                await _nannyConfig
                    .ErrorHandler
                    .Handle(RestartFunctionRun());

                KillAll();
            }
            catch (Exception)
            {
                KillAll();
            }



        }

        private void KillAll()
        {
            _nannyConfig.Cts.Cancel();
        }

        private Func<Task> RestartFunctionRun()
        {
            return async () => await _nannyConfig
                    .StartOptions
                    .Runner(ErrorHandledFunction(), _nannyConfig).ConfigureAwait(false);
        }

        private Func<Task> StartFunctionRun()
        {
            if (_startFunction == null) throw new NotImplementedException();

            return async () => await _startFunction(_nannyConfig.Cts.Token).ConfigureAwait(false);
        }

        private Func<CancellationToken, Task> ErrorHandledFunction()
        {
            if (_restartFunction == null) throw new NotImplementedException();

            return (token) => _nannyConfig.ErrorHandler.Handle(() => _restartFunction(token));
        }
    }
}
