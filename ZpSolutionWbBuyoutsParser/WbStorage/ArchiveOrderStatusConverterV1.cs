using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZpSolutionWbBuyoutsParser.WbStorage
{
    internal class ArchiveOrderStatusConverterV1 : IOrderStatusConverter
    {
        private readonly static string[] _rejectedStatuses = new string[] { "rejected", "order_rejected" };
        private readonly static string[] _refundStatuses = new string[] { "refund" };
        private readonly static string[] _excludedStatuses = new string[] { "excludedfromrate", "excluded_from_rate" };
        public string GetDbFormatStatus(string orderStatus)
        {
            string lowerStatus = orderStatus.ToLower();
            if (_rejectedStatuses.Contains(lowerStatus)) return "rejected";
            else if (_refundStatuses.Contains(lowerStatus)) return "refund";
            else if (_excludedStatuses.Contains(lowerStatus)) return "excludedFromRate";
            else return "complete";
        }
    }
}
