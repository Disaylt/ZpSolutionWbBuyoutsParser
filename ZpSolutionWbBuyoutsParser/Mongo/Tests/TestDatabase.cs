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
    }
}
