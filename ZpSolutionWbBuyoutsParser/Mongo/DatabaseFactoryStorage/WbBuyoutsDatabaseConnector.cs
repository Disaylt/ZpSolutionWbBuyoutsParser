using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZpSolutionWbBuyoutsParser.Mongo.DatabaseFactoryStorage
{
    internal class WbBuyoutsDatabaseConnector : DatabaseConnector
    {
        public override IMongoDatabase Database
        {
            get
            {
                return _database;
            }
        }

        private static IMongoDatabase _database;
        private const string _dbName = "wb_buyouts";

        public WbBuyoutsDatabaseConnector()
        {
            if (_database == null)
            {
                _database = GetDatabase(_dbName);
            }
        }
    }
}
