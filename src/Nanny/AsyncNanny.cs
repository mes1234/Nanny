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
                NannyConfig.Cts.Token.ThrowIfCancellationRequested();

                await _nannyConfig
                    .ErrorHandler
                    .Handle(StartFunctionRun(), _nannyConfig.Logger);

                if (_restartFunction != null)
                    await _nannyConfig
                        .ErrorHandler
                        .Handle(RestartFunctionRun(), _nannyConfig.Logger);

                KillAll();
            }
            catch (Exception)
            {
                KillAll();
            }
        }

        private static void KillAll()
        {
            NannyConfig.Cts.Cancel();
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

            return async () => await _startFunction(NannyConfig.Cts.Token).ConfigureAwait(false);
        }

        private Func<CancellationToken, Task> ErrorHandledFunction()
        {
            if (_restartFunction == null) throw new NotImplementedException();

            return (token) => _nannyConfig.ErrorHandler.Handle(() => _restartFunction(token));
        }
    }
}
