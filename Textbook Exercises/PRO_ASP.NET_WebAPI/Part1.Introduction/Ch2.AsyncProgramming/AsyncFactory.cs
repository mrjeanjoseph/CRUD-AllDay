using System.Threading.Tasks;
using System.Threading;

namespace AsyncProgramming
{
    public class AsyncFactory
    {

        public static Task<int> GetIntAsync(CancellationToken token)
        {
            var tcs = new TaskCompletionSource<int>();

            var timer = new System.Timers.Timer(2000);
            timer.AutoReset = false;
            timer.Elapsed += (s, e) =>
            {
                tcs.SetResult(10);
                timer.Dispose();
            };
            timer.Start();
            return tcs.Task;
        }

        public static Task<int> GetIntAsyncTwo() => Task.FromResult(100);

        public static Task<int> GetIntAsyncBogusOper(
            CancellationToken token = default)
        {
            var tcs = new TaskCompletionSource<int>();

            if (token.IsCancellationRequested)
            {
                tcs.SetCanceled();
                return tcs.Task;
            }
            var timer = new System.Timers.Timer(3000);
            timer.AutoReset = false;
            timer.Elapsed += (s, e) =>
            {
                tcs.TrySetResult(10);
                timer.Dispose();
            };
            if (token.CanBeCanceled)
            {
                token.Register(() =>
                {
                    tcs.TrySetCanceled();
                    timer.Dispose();
                });
            }
            timer.Start();
            return tcs.Task;
        }

        public static Task<int> InternalGetIntAsync(CancellationToken token)
        {
            var cts = new CancellationTokenSource(500);
            var linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, token);
            return GetIntAsync(linkedTokenSource.Token);
        }
    }

}
