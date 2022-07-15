using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZpSolutionWbBuyoutsParser.Models.Bson.WB;

namespace ZpSolutionWbBuyoutsParser.Mongo.Tests
{
    internal class TestCollection<T> : TestDatabase
    {
        private IMongoCollection<T> _mongoCollection;

        public TestCollection(string collectionName)
        {
            if (_mongoCollection == null)
            {
                _mongoCollection = DataBase.GetCollection<T>(collectionName);
            }
        }

        public void Insert(T products)
        {
            _mongoCollection.InsertOne(products);
        }
    }
}
