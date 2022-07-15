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
        private static IMongoCollection<ProductsModel> _mongoCollection;

        public WbProductsCollection()
        {
            if(_mongoCollection == null )
            {
                _mongoCollection = DataBase.GetCollection<ProductsModel>(_name);
            }
        }

        public void Insert(ProductsModel products)
        {
            _mongoCollection.InsertOne(products);
        }
    }
}
