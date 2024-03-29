﻿using Global.ZennoLab.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZpSolutionWbBuyoutsParser.Models.Json
{
    internal class ArchiveProductModel
    {
        [JsonProperty("code1S")]
        public int ProductId { get; set; }

        [JsonProperty("rId")]
        public string RId { get; set; } 

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("orderDate")]
        public DateTime OrderDate { get; set; }

        [JsonProperty("lastDate")]
        public DateTime LastDate { get; set; }
    }
}
