using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nanny
{
    /// <summary>
    ///  General purpose nanny for anything requiring attention
    ///  restarting
    ///  reconnecting
    ///  etc.
    /// </summary>
    public interface INanny
    {
        /// <summary>
        ///  Start function
        /// </summary>
        /// <param name="func">starting function</param>
        public INanny RegisterStart(Func<Task> func);

        /// <summary>
        /// Function triggered when start function completes 
        /// </summary>
        /// <param name="func"></param>
        public INanny RegisterRestart(Func<Task> func);

        /// <summary>
        /// Start babysitting
        /// </summary>
        /// <exception cref="NotImplementedException">No start/stop prcedure was registered</exception>
        /// <returns>awaitable Task</returns>
        public Task StartAsync(CancellationTokenSource cts);
    }
}
