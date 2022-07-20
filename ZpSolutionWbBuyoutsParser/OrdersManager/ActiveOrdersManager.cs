using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZpSolutionWbBuyoutsParser.Mongo;
using ZpSolutionWbBuyoutsParser.Parser;
using ZpSolutionWbBuyoutsParser.WbStorage;
using ZpSolutionWbBuyoutsParser.ZennoPoster;

namespace ZpSolutionWbBuyoutsParser.OrdersManager
{
    internal class ActiveOrdersManager
    {
        private readonly WbAccountOrdersParser _orderParser;
        private readonly WbProductsCollection _productCollection;
        private readonly IOrderStatusConverter _archiveOrderStatusConverter;
        private readonly ZennoPosterProfile _zennoPosterProfile;

        public ActiveOrdersManager(WbAccountOrdersParser ordersParser, ZennoPosterProfile zennoPosterProfile)
        {
            _orderParser = ordersParser;
            _zennoPosterProfile = zennoPosterProfile;
            _productCollection = new WbProductsCollection();
            _archiveOrderStatusConverter = new ArchiveOrderStatusConverterV1();
        }
    }
}
