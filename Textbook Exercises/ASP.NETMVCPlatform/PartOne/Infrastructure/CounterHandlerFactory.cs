using System.Collections.Concurrent;
using System.Web;

namespace PartOne.Infrastructure
{
    public class CounterHandlerFactory : IHttpHandlerFactory
    {
        private int _counter = 0, _handlerMaxCount = 3, _handlerCount = 0;
        private BlockingCollection<CounterHandler> pool = new BlockingCollection<CounterHandler>();

        public IHttpHandler GetHandler(HttpContext context, string verb, string url, string path)
        {
            CounterHandler handler;

            if (!pool.TryTake(out handler)) 
            {
                if (_handlerCount < _handlerMaxCount)
                {
                    _handlerCount++;
                    handler = new CounterHandler(++_counter);
                    pool.Add(handler);
                }
                else handler = pool.Take();
            }
            return handler;

            //if (context.Request.UserAgent.Contains("Chrome"))
            //    return new SiteLengthHandler();
            //else return new CounterHandler(++_counter);
        }

        public void ReleaseHandler(IHttpHandler handler)
        {
            if (handler.IsReusable) pool.Add((CounterHandler)handler);
            else _handlerCount--;
        }
    }
}