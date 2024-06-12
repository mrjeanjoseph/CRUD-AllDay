using System.Web;

namespace PartOne.Infrastructure
{
    public class CounterHandler : IHttpHandler
    {
        private int _counter, _requestCounter = 0;

        public CounterHandler(int counter)
        {
            _counter = counter;
        }

        public void ProcessRequest(HttpContext context)
        {
            _requestCounter++;
            context.Response.ContentType = "text/plain";
            context.Response.Write(string.Format(
                "The counter value is {0} (Request {1} of 3)", _counter, _requestCounter));
        }
        public bool IsReusable 
        { 
            get { return _requestCounter < 2; } 
        }
    }
}