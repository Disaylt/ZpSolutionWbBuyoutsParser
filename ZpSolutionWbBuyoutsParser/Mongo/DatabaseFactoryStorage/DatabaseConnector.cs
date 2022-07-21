using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZpSolutionWbBuyoutsParser.Mongo.DatabaseFactoryStorage
{
    internal abstract class DatabaseConnector : MongoConnector
    {
        public abstract IMongoDatabase Database { get; }
    }
}
