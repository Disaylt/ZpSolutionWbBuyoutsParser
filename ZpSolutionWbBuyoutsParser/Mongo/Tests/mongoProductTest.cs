using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZpSolutionWbBuyoutsParser.Models.Bson.WB;

namespace ZpSolutionWbBuyoutsParser.Mongo.Tests
{
    internal class MongoProductTest
    {
        private const string _collectionName = "wb_products_test";
        private static TestCollection<ProductsModel> _collection;
        public MongoProductTest()
        {
            if( _collection == null )
            {
                _collection = new TestCollection<ProductsModel>(_collectionName);
            }
        }

        public void ExcecuteTest()
        {
            ProductsModel productsModel = CreteTestModel();
            _collection.Insert( productsModel );
        }

        private ProductsModel CreteTestModel()
        {
            ProductsModel model = new ProductsModel
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
                ReviewExists = false,
                RID = "123123123",
                Session = "sadasd",
                Status = "complete",
                Title = "Title"
            };
            return model;
        }
    }
}
