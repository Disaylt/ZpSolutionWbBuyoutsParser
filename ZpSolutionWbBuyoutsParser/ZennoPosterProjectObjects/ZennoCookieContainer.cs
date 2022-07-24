using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ZennoLab.InterfacesLibrary.ProjectModel;

namespace ZpSolutionWbBuyoutsParser.ZennoPosterProjectObjects
{
    internal class ZennoCookieContainer : CookieContainer
    {
        private readonly CookieCollection _cookieCollection;
        public ZennoCookieContainer(ICookieContainer cookieContainer) : base(200, 50, 4096)
        {
            _cookieCollection = ConvertToCookieCollection(cookieContainer);
            Add(_cookieCollection);
        }

        private CookieCollection ConvertToCookieCollection(ICookieContainer cookieContainer)
        {
            CookieCollection cookieCollection = new CookieCollection();
            foreach (string domain in cookieContainer.Domains)
            {
                var domainCookies = cookieContainer.Get(domain);
                CookieCollection domainCookieCollection = GetDomainCollectionCookies(domainCookies);
                cookieCollection.Add(domainCookieCollection);
            }
            return cookieCollection;
        }

        private CookieCollection GetDomainCollectionCookies(IEnumerable<ICookieItem> domainCookies)
        {
            CookieCollection domainCookieCollection = new CookieCollection();
            foreach (var domainCookie in domainCookies)
            {
                Cookie cookie = new Cookie(
                    domainCookie.Name,
                    domainCookie.Value,
                    domainCookie.Path,
                    domainCookie.Host
                    );
                domainCookieCollection.Add(cookie);
            }
            return domainCookieCollection;
        }
    }
}
