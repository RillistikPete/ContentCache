using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContentCache.Infrastructure
{
    public enum AppStateKeys
    {
        INDEX_COUNTER
    }
    public static class AppStateHelper
    {
        public static int IncrementAndGet(AppStateKeys key)
        {
            string keyString = Enum.GetName(typeof(AppStateKeys), key);
            HttpApplicationState state = HttpContext.Current.Application;
            return (int)(state[keyString] = ((int)(state[keyString] ?? 0) + 1)); 
        }

    }
}