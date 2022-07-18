using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ZpSolutionWbBuyoutsParser.Models.Bson.WB
{
    internal class PlanningModel
    {
        [BsonId]
        public BsonObjectId Id { get; set; }

        [BsonElement("date")]
        public DateTime CreateDate { get; set; }

        [BsonElement("session")]
        public string Session { get; set; }
    }
}
