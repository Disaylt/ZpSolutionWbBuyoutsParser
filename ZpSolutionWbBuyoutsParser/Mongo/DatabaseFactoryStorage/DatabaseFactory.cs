using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZpSolutionWbBuyoutsParser.Mongo.DatabaseFactoryStorage
{
    internal abstract class DatabaseFactory
    {
        public abstract DatabaseConnector GetDbConnector();
    }
}
