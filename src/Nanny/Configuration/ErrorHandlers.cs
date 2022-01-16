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
    public class ErrorHandlers
    {
        public ErrorHandlers(bool catchFlag, bool logFlag, bool continueFlag)
        {
            CatchFlag = catchFlag;
            LogFlag = logFlag;
            ContinueFlag = continueFlag;
        }

        public readonly static ErrorHandlers CatchLogContinue = new ErrorHandlers(true, true, true);

        public readonly static ErrorHandlers ThrowAndLog = new ErrorHandlers(false, true, false);

        public bool CatchFlag { get; }
        public bool LogFlag { get; }
        public bool ContinueFlag { get; }

        public async Task Handle(Func<Task> func, ILogger? logger = null)
        {
            var _logger = logger ?? NullLogger.Instance;
            try
            {
                await func().ConfigureAwait(false);
            }
            catch (OperationCanceledException ex)
            {
                if (LogFlag)
                    _logger.LogError("Operation was cancelled {Msg}", ex.Message);

                throw;
            }
            catch (Exception ex)
            {
                if (LogFlag)
                    _logger.LogError("Error occured {Msg}", ex.Message);

                if (!CatchFlag)
                    throw;

                if (!ContinueFlag)
                    throw new InvalidOperationException("Nanny cannot continue");
            }
        }

    }
}
