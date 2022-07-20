using Global.ZennoLab.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZpSolutionWbBuyoutsParser.Models.Json
{
    internal class ActiveOrdersModel
    {
        [JsonProperty("positions")]
        List<ActiveOrderModel> ActiveOrders { get; set; } = new List<ActiveOrderModel>();

        [JsonProperty("locations")]
        Dictionary<string, DeliveryPointInfoModel> DeliveryPoints { get; set; } = new Dictionary<string, DeliveryPointInfoModel>();

        [JsonProperty("privateCode")]
        public string PrivateCode { get; set; }
    }
}
