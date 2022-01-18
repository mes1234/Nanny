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
    public class ErrorOptions
    {
        public ErrorOptions(Func<Func<Task>, ILogger, Task> handle)
        {
            Handle = handle ?? throw new ArgumentNullException(nameof(handle));
        }

        public readonly static ErrorOptions CatchLogContinue = new ErrorOptions(ErrorHandlers.CatchLogContinue);

        public readonly static ErrorOptions ThrowAndLog = new ErrorOptions(ErrorHandlers.CatchLogThrow);

        public Func<Func<Task>, ILogger, Task> Handle { get; }
    }
}
