using System;
using System.Web.Caching;

namespace CreatingStatefulData.Infrastructure
{
    public class SelfExpiringData<T> : CacheDependency
    {
        private T davaValue;
        private int requestCount = 0;
        private int requestLimit;

        public T Value
        {
            get
            {
                if (requestCount++ >= requestLimit)
                    NotifyDependencyChanged(this, EventArgs.Empty);

                return davaValue;
            }
        }

        public SelfExpiringData(T data, int limit)
        {
            davaValue = data;
            requestLimit = limit;
        }
    }
}