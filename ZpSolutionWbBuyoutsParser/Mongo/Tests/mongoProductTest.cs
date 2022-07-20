using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZpSolutionWbBuyoutsParser.Models.Bson.WB;

namespace ZpSolutionWbBuyoutsParser.Mongo.Tests
{
    internal class MongoProductTest : TestDatabase
    {
        private const string _collecectionName = "wb_products_test";
        public IMongoCollection<ProductModel> MongoCollection { get; }

        public MongoProductTest()
        {
            if (MongoCollection == null)
            {
                MongoCollection = DataBase.GetCollection<ProductModel>(_collecectionName);
            }
        }

        public List<ProductModel> GetProducts()
        {
            BsonDocument emptyFilter = new BsonDocument();
            var products = MongoCollection.FindAsync<ProductModel>(emptyFilter)
                .Result
                .ToList();
            return products;
        }

        public void Insert(ProductModel products)
        {
            MongoCollection.InsertOne(products);
        }

        public ProductModel CreteTestModel()
        {
            ProductModel model = new ProductModel
            {
                Address = "test address",
                IsActive = true,
                Brand = "test brand",
                BuyoutsDate = DateTime.Now,
                LastCheck = DateTime.Now,
                Code = "123",
                LastUpdate = DateTime.Now,
                OrderDate = DateTime.Now,
                Price = 123,
                ProductId = 21321312,
                ReciveDate = "test date",
                RID = "123123123",
                Session = "sadasd",
                Status = "complete",
                Title = "Title"
            };
            return model;
        }
    }
}