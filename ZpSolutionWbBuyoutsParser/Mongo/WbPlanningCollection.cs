using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZpSolutionWbBuyoutsParser.Models.Bson.WB;
using ZpSolutionWbBuyoutsParser.Mongo.DatabaseFactoryStorage;

namespace ZpSolutionWbBuyoutsParser.Mongo
{
    internal class WbPlanningCollection
    {
        private const string _name = "planning";

        private static IMongoCollection<BsonDocument> _mongoCollection;

        public WbPlanningCollection()
        {
            if (_mongoCollection == null)
            {
                DatabaseFactory databaseFactory = new WbBuyoutsDatabaseFactory();
                var dbConnector = databaseFactory.GetDbConnector();
                _mongoCollection = dbConnector.Database.GetCollection<BsonDocument>(_name);
            }
        }

        public List<string> GetUniqueAccountsForLastDays(int days)
        {
            DateTime startDate = DateTime.Now.AddDays(-1 * days);
            var filter = Builders<BsonDocument>.Filter.Gt("date", startDate);
            var planningRows = _mongoCollection
                .Find(filter)
                .ToList();
            var uniqueSessions = planningRows
                .Select(x => x["session"].AsString)
                .Distinct()
                .ToList();
            return uniqueSessions;
        }
    }
}
