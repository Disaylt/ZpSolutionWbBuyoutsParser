using Global.ZennoLab.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ZennoLab.InterfacesLibrary.ProjectModel.Collections;
using ZpSolutionWbBuyoutsParser.Models.Http;
using ZpSolutionWbBuyoutsParser.Models.Json;

namespace ZpSolutionWbBuyoutsParser.Parser
{
    internal class WbAccountOrdersParser
    {
        private readonly HttpRequestSender _httpRequestSender;
        public WbAccountOrdersParser(IWebProxy webProxy, IProfile profile)
        {
            _httpRequestSender = CreateWbRequestSender(webProxy, profile);
        }

        public List<ArchiveProductModel> GetArchiveProducts()
        {
            StringContent requestBody = new StringContent("limit=150&type=all");
            RequestContentModel requestContent = new RequestContentModel("application/x-www-form-urlencoded; charset=UTF-8", requestBody);
            string url = "https://www.wildberries.ru/webapi/lk/myorders/archive/get";
            string responseContent = _httpRequestSender.SendAsync(HttpMethod.Post, url, requestContent).Result;
            List<ArchiveProductModel> products = JToken.Parse(responseContent)["value"]["archive"].ToObject<List<ArchiveProductModel>>();
            return products;
        }

        private HttpRequestSender CreateWbRequestSender(IWebProxy webProxy, IProfile profile)
        {
            ZpHttpClient zpHttpClient = new ZpHttpClient(profile, webProxy);
            IHeaderInstaller headerInstaller = new WbHeaderInstaller(profile);
            HttpRequestSender httpRequestSender = new HttpRequestSender(zpHttpClient, headerInstaller);
            return httpRequestSender;
        }
    }
}
