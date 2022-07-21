using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoLab.InterfacesLibrary.ProjectModel.Collections;
using ZpSolutionWbBuyoutsParser.ZennoPoster;

namespace ZpSolutionWbBuyoutsParser.Parser
{
    internal class ZpHttpClient : HttpClientHandler
    {
        public string UserAgent { get; }
        public ZpHttpClient(IProfile profile, IWebProxy proxy)
        {
            CookieContainer = new ZennoCookieContainer(profile.CookieContainer);
            UseProxy = true;
            Proxy = proxy;
            UserAgent = profile.UserAgent;
        }
    }
}
