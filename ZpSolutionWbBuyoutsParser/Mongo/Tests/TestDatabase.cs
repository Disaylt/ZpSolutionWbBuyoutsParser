using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZpSolutionWbBuyoutsParser.Mongo.Tests
{
    internal class TestDatabase : MongoConnector
    {
        protected IMongoDatabase DataBase
        {
            get
            {
                return _database;
            }
        }

        private static IMongoDatabase _database;
        private const string _dbName = "test";

        public TestDatabase()
        {
            if (_database == null)
            {
                _database = GetDatabase(_dbName);
            }
        }

        public DateTime GetDbTime()
        {
            var db = GetDatabase("admin");
            var serverStatusCmd = new BsonDocumentCommand<BsonDocument>(new BsonDocument { { "serverStatus", 1 } });
            var result = db.RunCommand(serverStatusCmd);
            DateTime localTime = result["localTime"].ToLocalTime();
            return localTime;
        }
    }
}
