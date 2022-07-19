using Global.ZennoLab.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZpSolutionWbBuyoutsParser.Models.Json
{
    internal class ArchiveProductsModel
    {
        [JsonProperty("code1S")]
        public int ProductId { get; set; }

        [JsonProperty("rId")]
        public string OrderId { get; set; } 
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("orderDate")]
        public DateTime OrderDate { get; set; }

        [JsonProperty("lastDate")]
        public DateTime lastDate { get; set; }
    }
}
