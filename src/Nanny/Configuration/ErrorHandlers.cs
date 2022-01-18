using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nanny.Configuration
{

    internal static class ErrorHandlers
    {
        public readonly static Func<Func<Task>, ILogger, Task> CatchLogContinue = async (func, logger) =>
        {
            try
            {
                await func().ConfigureAwait(false);
            }
            catch (OperationCanceledException ex)
            {
                logger.LogError("Operation was cancelled {Msg}", ex.Message);

                throw;
            }
            catch (Exception ex)
            {
                logger.LogError("Error occured {Msg}", ex.Message);
            }
        };

        public readonly static Func<Func<Task>, ILogger, Task> CatchLogThrow = async (func, logger) =>
        {
            try
            {
                await func().ConfigureAwait(false);
            }
            catch (OperationCanceledException ex)
            {
                logger.LogError("Operation was cancelled {Msg}", ex.Message);

                throw;
            }
            catch (Exception ex)
            {
                logger.LogError("Error occured {Msg}", ex.Message);

                throw;
            }
        };
    }
}
