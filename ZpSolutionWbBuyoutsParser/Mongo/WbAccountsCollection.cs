using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZpSolutionWbBuyoutsParser.Mongo.DatabaseFactoryStorage;

namespace ZpSolutionWbBuyoutsParser.Mongo
{
    internal class WbAccountsCollection
    {
        private const string _name = "accounts";

        private static IMongoCollection<BsonDocument> _mongoCollection;

        public WbAccountsCollection()
        {
            if (_mongoCollection == null)
            {

                DatabaseFactory databaseFactory = new WbBuyoutsDatabaseFactory();
                var dbConnector = databaseFactory.GetDbConnector();
                _mongoCollection = dbConnector.Database.GetCollection<BsonDocument>(_name);
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
