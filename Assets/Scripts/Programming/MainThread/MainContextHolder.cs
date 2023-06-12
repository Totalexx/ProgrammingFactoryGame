using System;
using System.Threading;

namespace Programming.MainThread
{
    public static class MainContextHolder
    {
        public static Thread MainThread { get; } = Thread.CurrentThread;
        public static SynchronizationContext SynchronizationContext { get; } = SynchronizationContext.Current;

        public static void Initialize() { }

        public static void RunInMain(SendOrPostCallback action)
        {
            SynchronizationContext.Post(action, null);
        }
    }
}