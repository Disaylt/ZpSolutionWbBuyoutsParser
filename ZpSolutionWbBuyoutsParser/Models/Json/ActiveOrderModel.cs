using Global.ZennoLab.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZpSolutionWbBuyoutsParser.Models.Json
{
    internal class ActiveOrderModel
    {
        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("locationId")]
        public string LocationId { get; set; }

        [JsonProperty("rId")]
        public string RId { get; set; }

        [JsonProperty("orderDate")]
        public DateTime OrderDate { get; set; }

        [JsonProperty("name")]
        public string Title { get; set; }
    }
}
