using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZpSolutionWbBuyoutsParser.Mongo.DatabaseFactoryStorage
{
    internal class WbBuyoutsDatabaseFactory : DatabaseFactory
    {
        public override DatabaseConnector GetDbConnector()
        {
            return new WbBuyoutsDatabaseConnector();
        }
    }
}
