using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using ZpSolutionWbBuyoutsParser.Models.Bson.WB;
using ZpSolutionWbBuyoutsParser.Mongo.DatabaseFactoryStorage;

namespace ZpSolutionWbBuyoutsParser.Mongo.CollectionStorage
{
    internal class WbProductsCollection
    {
        private const string _name = "products";
        private static IMongoCollection<ProductModel> _mongoCollection;

        public WbProductsCollection()
        {
            if(_mongoCollection == null )
            {
                WbBuyoutsDatabaseFactory databaseFactory = new WbBuyoutsDatabaseFactory();
                var dbConnector = databaseFactory.GetDbConnector();
                _mongoCollection = dbConnector.Database.GetCollection<ProductModel>(_name);
            }
        }

        public void Insert(ProductModel products)
        {
            _mongoCollection.InsertOne(products);
        }

        public ProductModel FindProduct(string rId)
        {
            var filter = Builders<ProductModel>.Filter.Eq("rid", rId);
            var products = _mongoCollection.Find(filter).FirstOrDefault();
            return products;
        }

        public void UpdateCheckDate(BsonObjectId id)
        {
            var filter = Builders<ProductModel>.Filter.Eq("_id", id);
            _mongoCollection.UpdateOne(filter, new BsonDocument("$set", new BsonDocument("last_check", DateTime.Now.AddHours(3))));
        }

        public void Replace(ProductModel product)
        {
            var filter = Builders<ProductModel>.Filter.Eq("_id", product.Id);
            product.LastUpdate = DateTime.Now.AddHours(3);
            product.LastCheck = DateTime.Now.AddHours(3);
            _mongoCollection.ReplaceOne(filter, product);
        }
    }
}
