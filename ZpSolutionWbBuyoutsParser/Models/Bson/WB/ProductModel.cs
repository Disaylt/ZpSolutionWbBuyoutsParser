using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ZpSolutionWbBuyoutsParser.Models.Bson.WB
{
    internal class ProductModel
    {
        [BsonId]
        public BsonObjectId Id { get; set; }

        [BsonElement("session")]
        public string Session { get; set; }

        [BsonElement("order_date")]
        public DateTime OrderDate { get; set; }

        [BsonElement("buyout_date")]
        public DateTime BuyoutsDate { get; set; }

        [BsonElement("status")]
        public string Status { get; set; }

        [BsonElement("recive_date")]
        public string ReciveDate { get; set; }

        [BsonElement("address")]
        public string Address { get; set; }

        [BsonElement("rid")]
        public string RID { get; set; }

        [BsonElement("product_id")]
        public long ProductId { get; set; }

        [BsonElement("brand")]
        public string Brand { get; set; }

        [BsonElement("price")]
        public int Price { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("last_update")]
        public DateTime LastUpdate { get; set; }

        [BsonElement("review_date")]
        public object ReviewDate { get; set; }

        [BsonElement("cancel_date")]
        public object CancelDate { get; set; }

        [BsonElement("code")]
        public string Code { get; set; }

        [BsonElement("last_check")]
        public DateTime LastCheck { get; set; }

        [BsonElement("review_exists")]
        public bool ReviewExists { get; set; }

        [BsonElement("isActive")]
        public bool IsActive { get; set; }
    }
}
