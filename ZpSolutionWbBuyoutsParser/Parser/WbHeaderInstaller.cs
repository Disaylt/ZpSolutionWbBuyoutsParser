using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ZennoLab.InterfacesLibrary.ProjectModel.Collections;

namespace ZpSolutionWbBuyoutsParser.Parser
{
    internal class WbHeaderInstaller : IHeaderInstaller
    {
        private readonly IProfile _zpProfile;
        public WbHeaderInstaller(IProfile zpProfile)
        {
            _zpProfile = zpProfile;
        }

        public void SetHeaders(HttpRequestMessage request)
        {
            request.Headers.TryAddWithoutValidation("User-Agent", _zpProfile.UserAgent);
            request.Headers.TryAddWithoutValidation("Accept", "*/*");
            request.Headers.TryAddWithoutValidation("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
            request.Headers.TryAddWithoutValidation("Accept-Encoding", "UTF-8");
            request.Headers.TryAddWithoutValidation("x-client-time", DateTime.Now.ToLongDateString());
            request.Headers.TryAddWithoutValidation("x-requested-with", "XMLHttpRequest");
            request.Headers.TryAddWithoutValidation("x-spa-version", "9.3.9.1");
            request.Headers.TryAddWithoutValidation("Origin", "https://www.wildberries.ru");
            request.Headers.TryAddWithoutValidation("Content-Length", "0");
            request.Headers.TryAddWithoutValidation("Connection", "keep-alive");
            request.Headers.TryAddWithoutValidation("Sec-Fetch-Dest", "empty");
            request.Headers.TryAddWithoutValidation("Sec-Fetch-Mode", "cors");
            request.Headers.TryAddWithoutValidation("Sec-Fetch-Site", "same-origin");
            request.Headers.TryAddWithoutValidation("Pragma", "no-cache");
            request.Headers.TryAddWithoutValidation("Cache-Control", "no-cache");
            request.Headers.TryAddWithoutValidation("TE", "trailers");
        }


    }
}
