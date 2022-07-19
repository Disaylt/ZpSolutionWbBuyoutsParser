using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SmartProxyV2_ZennoLabVersion;
using SmartProxyV2_ZennoLabVersion.Models;
using SmartProxyV2_ZennoLabVersion.MongoModels;

namespace ZpSolutionWbBuyoutsParser.Proxy
{
    internal class ProxyStream : IDisposable
    {
        private readonly ProxyModel _proxy;
        public ProxyStream()
        {
            _proxy = TakeProxy();
        }

        public IWebProxy GetProxy()
        {
            IWebProxy webProxy = new WebProxy(_proxy.Ip, _proxy.PortData.PortNum);
            ICredentials credentials = new NetworkCredential(_proxy.User, _proxy.Password);
            webProxy.Credentials = credentials;
            return webProxy;
        }

        public void OpenPort()
        {
            SmartProxyHandler.OpenPortAsync(_proxy.PortData);
        }

        public void Dispose()
        {
            OpenPort();
        }

        private ProxyModel TakeProxy()
        {
            ProxyModel proxy = SmartProxyHandler
                .GetRussianProxyAsync()
                .Result;
            return proxy;
        }
    }
}
