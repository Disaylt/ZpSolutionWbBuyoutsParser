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

        private static IMongoCollection<PlanningModel> _mongoCollection;

        public WbPlanningCollection()
        {
            if (_mongoCollection == null)
            {
                _mongoCollection = DataBase.GetCollection<PlanningModel>(_name);
            }
        }

        public List<string> GetUniqueAccountsForLastDays(int days)
        {
            DateTime startDate = DateTime.Now.AddDays(-1 * days);
            var planningRows = _mongoCollection
                .Find(x => x.CreateDate > startDate)
                .ToListAsync()
                .Result;
            var uniqueSessions = planningRows
                .Select(x => x.Session)
                .Distinct()
                .ToList();
            return uniqueSessions;
        }
    }
}
