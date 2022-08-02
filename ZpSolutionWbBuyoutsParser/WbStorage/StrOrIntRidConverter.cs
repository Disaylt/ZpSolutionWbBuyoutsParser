using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZpSolutionWbBuyoutsParser.Models.Standard;

namespace ZpSolutionWbBuyoutsParser.WbStorage
{
    internal class StrOrIntRidConverter : IRidConverter<RidTypesValueModel>
    {
        public RidTypesValueModel ConvertToDifferentTypes(string rid)
        {
            RidTypesValueModel ridTypesValueModel = new RidTypesValueModel();
            if(long.TryParse(rid, out long value))
            {
                ridTypesValueModel.Rid = value;
                ridTypesValueModel.SRid = BsonNull.Value;
            }
            else
            {
                ridTypesValueModel.Rid = BsonNull.Value;
                ridTypesValueModel.SRid = rid;
            }
            return ridTypesValueModel;
        }
    }
}