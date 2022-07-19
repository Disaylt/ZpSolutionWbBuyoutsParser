﻿using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZpSolutionWbBuyoutsParser.Models.Bson.WB;

namespace ZpSolutionWbBuyoutsParser.Mongo
{
    internal class WbPlanningCollection : WbBuyoutsDatabase
    {
        private const string _name = "planning";

        private static IMongoCollection<BsonDocument> _mongoCollection;

        public WbPlanningCollection()
        {
            if (_mongoCollection == null)
            {
                _mongoCollection = DataBase.GetCollection<BsonDocument>(_name);
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