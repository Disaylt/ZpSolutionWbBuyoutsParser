using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZpSolutionWbBuyoutsParser.Mongo.DatabaseFactory
{
    internal abstract class MongoDatabase : MongoConnector
    {
        public abstract IMongoDatabase Database { get; }
    }
}
