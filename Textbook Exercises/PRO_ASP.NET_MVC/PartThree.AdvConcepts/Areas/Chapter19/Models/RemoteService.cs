using System;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter19.ControllerExtensibility.Models
{
    internal class RemoteService
    {
        internal string GetRemoteData()
        {
            Thread.Sleep(3000);
            return "Hello from there to here, from here to there";
        }

        internal async Task<string> GetRemoteDataSync()
        {
            return await Task<string>.Factory.StartNew(() =>
            {
                Thread.Sleep(3000);
                return "Hello from there to here, from here to there. GetRemoteDataSync";
            });
        }
    }
}