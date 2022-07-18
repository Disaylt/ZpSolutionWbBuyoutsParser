using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using ZpSolutionWbBuyoutsParser.Models.Bson.WB;

namespace ZpSolutionWbBuyoutsParser.Mongo
{
    internal class WbProductsCollection : WbBuyoutsDatabase
    {
        private const string _name = "products";
        private static IMongoCollection<ProductModel> _mongoCollection;

        public WbProductsCollection()
        {
            if(_mongoCollection == null )
            {
                _mongoCollection = DataBase.GetCollection<ProductModel>(_name);
            }
        }

        public void Insert(ProductModel products)
        {
            _mongoCollection.InsertOne(products);
        }
    }
}
