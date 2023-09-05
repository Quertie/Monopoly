using System;
using UnityEngine;

namespace Helpers
{
    public static class MainThreadDispatcherHelper
    {
        public static void EnqueueAsync(Action<MonoBehaviour> action)
        {
            var mainThreadDispatcher = UnityMainThreadDispatcher.Instance();
            mainThreadDispatcher.EnqueueAsync(() => action.Invoke(mainThreadDispatcher));
        }
    }
}