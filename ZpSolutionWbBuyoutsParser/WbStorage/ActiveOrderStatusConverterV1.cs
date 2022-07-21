using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZpSolutionWbBuyoutsParser.WbStorage
{
    internal class ActiveOrderStatusConverterV1 : IOrderActiveStatusConverter
    {
        public string GetDbFormatStatus(bool orderStatus)
        {
            if (orderStatus) return "ready_to_receive";
            else return "en_route";
        }
    }
}
