using System.Linq;
using System.Net.Http;
using System.Net.Http.Extensions.Compression.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClientConcepts
{
    public class CompressHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return base.SendAsync(request, cancellationToken)
                .ContinueWith<HttpResponseMessage>((responseToCompleteTask) =>
                {
                    var response = responseToCompleteTask.Result;

                    if (response.RequestMessage != null && response.RequestMessage.Headers.AcceptEncoding.Any())
                    {
                        var contentEncoding = response.RequestMessage.Headers.AcceptEncoding.First().Value;
                        if (null != response.Content)
                            response.Content = new CompressedContent(response.Content, null);
                    }
                    return response;
                }, TaskContinuationOptions.OnlyOnRanToCompletion);
        }
    }
}
