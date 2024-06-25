using System;
using System.Web;

namespace CachingContent.Infrastructure
{
    public enum AppStateKeys
    {
        INDEX_COUNTER,
    }

    public class AppStateHelper
    {
        public static int IncrementAndGet(AppStateKeys keys)
        {
            string keystring = Enum.GetName(typeof(AppStateKeys), keys);
            HttpApplicationState state = HttpContext.Current.Application;
            var result = (int)(state[keystring] = (int)(state[keystring] ?? 0) + 1);
            return result;
        }
    }
}