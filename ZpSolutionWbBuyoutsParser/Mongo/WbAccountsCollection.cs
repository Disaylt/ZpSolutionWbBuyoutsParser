using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZpSolutionWbBuyoutsParser.Mongo
{
    internal class WbAccountsCollection : WbBuyoutsDatabase
    {
        private const string _name = "accounts";

        private static IMongoCollection<BsonDocument> _mongoCollection;

        public WbAccountsCollection()
        {
            if (_mongoCollection == null)
            {
                _mongoCollection = DataBase.GetCollection<BsonDocument>(_name);
            }
        }

        public List<string> GetWorkingAccounts()
        {
            var filter = new BsonDocument("is_active", true);
            List<string> accounts = _mongoCollection
                .Find(filter)
                .ToList()
                .Select(x => x["session"].AsString)
                .ToList();
            return accounts;
        }
    }
}
