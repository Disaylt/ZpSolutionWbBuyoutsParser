using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace ZpSolutionWbBuyoutsParser.Mongo
{
    internal abstract class MongoConnector
    {
        private static MongoClient _client;

        public MongoConnector()
        {
            if( _client == null )
            {
                _client = CreateClient();
            }
        }

        protected IMongoDatabase GetDatabase(string name)
        {
            IMongoDatabase database = _client.GetDatabase(name);
            return database;
        }

        private MongoClient CreateClient()
        {
            MongoSettings settings = new MongoSettings();
            string connectString = settings.GetSettings().ConnectionString;
            MongoClient client = new MongoClient( connectString );
            return client;
        }
    }
}
