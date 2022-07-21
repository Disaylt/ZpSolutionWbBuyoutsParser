using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace ZpSolutionWbBuyoutsParser.Mongo
{
    internal class WbBuyoutsDatabase : MongoConnector
    {
        protected IMongoDatabase DataBase
        {
            get
            {
                return _database;
            }
        }

        private static IMongoDatabase _database;
        private const string _dbName = "wb_buyouts";

        public WbBuyoutsDatabase()
        {
            if(_database == null)
            {
                _database = GetDatabase(_dbName);
            }    
        }
    }
}
