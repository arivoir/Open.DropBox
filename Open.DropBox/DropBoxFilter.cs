using Open.Net.Http;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Open.DropBox
{
    public class DropBoxMessageHandler : RetryMessageHandler
    {
        private static Random _rand = new Random();

        public DropBoxMessageHandler(IHttpMessageHandlerFactory messageHandlerFactory = null, int retriesCount = 5)
            : base(messageHandlerFactory, retriesCount)
        {
        }

        public DropBoxMessageHandler(HttpMessageHandler innerFilter, int retriesCount = 5)
            : base(innerFilter, retriesCount)
        {

        }

        protected override async Task<TimeSpan?> ShouldRetry(HttpResponseMessage response, int retries, CancellationToken cancellationToken)
        {
            var baseResult = await base.ShouldRetry(response, retries, cancellationToken);
            if (baseResult.HasValue)
                return baseResult.Value;
            if (response.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                var timeSpan = Math.Pow(2, retries) * 1000 + _rand.Next(0, 1000);
                return TimeSpan.FromMilliseconds(timeSpan);
            }
            return null;
        }
    }
}
