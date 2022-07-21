using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZpSolutionWbBuyoutsParser.Mongo.DatabaseFactory
{
    internal class TestConnector : DatabaseConnector
    {
        public override MongoDatabase GetDatabase()
        {
            return new TestDatabase();
        }
    }
}
