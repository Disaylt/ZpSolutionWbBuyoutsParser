using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZpSolutionWbBuyoutsParser.Models.Standard
{
    internal class RidTypesValueModel
    {
        public BsonValue Rid { get; set; }
        public BsonValue SRid { get; set; }
    }
}
