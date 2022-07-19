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
        protected ZpHttpClient HttpClientHandler { get; }
        private readonly IHeaderInstaller _headerInstaller;
        public HttpRequestSender(ZpHttpClient httpClientHandler, IHeaderInstaller headerInstaller)
        {
            HttpClientHandler = httpClientHandler;
            _headerInstaller = headerInstaller;
        }

        public async Task<string> Send(HttpMethod httpMethod, string url)
        {
            var client = new HttpClient(HttpClientHandler);
            using(var request = new HttpRequestMessage(httpMethod, url))
            {
                _headerInstaller.SetHeaders(request);
                var response = await client.SendAsync(request);
                string responseContent = await response.Content.ReadAsStringAsync();
                return responseContent;
            }
        }

        public async Task<string> SendWithBody()
    }
}
