using System;
using System.Threading;

namespace FeedR.Commons.Scheduling
{
    /// <summary>
    /// Manages periodic tasks.
    /// </summary>
    public abstract class PeriodicRunner
    {
        /// <summary>
        /// Will start a task represented by action with milliseconds interval.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="milliInterval"></param>
        /// <returns></returns>
        public CancellationTokenSource StartTask(Action action, int milliInterval)
        {
            var cts = new CancellationTokenSource();
            TimerTaskFactory.Start(action, milliInterval, cancelToken: cts.Token);

            // Make this task cancellable.
            return cts;
        }
    }
}
