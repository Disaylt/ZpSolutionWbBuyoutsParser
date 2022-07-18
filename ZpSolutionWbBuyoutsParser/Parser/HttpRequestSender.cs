using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ZpSolutionWbBuyoutsParser.Parser
{
    internal class HttpRequestSender
    {
        protected ZpHttpClient HttpClientHandler { get; private set; }
        public HttpRequestSender(ZpHttpClient httpClientHandler)
        {
            HttpClientHandler = httpClientHandler;
        }

        public async Task<string> Send(HttpMethod httpMethod, string url)
        {
            var client = new HttpClient(HttpClientHandler);
            using(var request = new HttpRequestMessage(httpMethod, url))
            {
                request.Headers.TryAddWithoutValidation("User-Agent", HttpClientHandler.UserAgent);
            }
        }
    }
}
