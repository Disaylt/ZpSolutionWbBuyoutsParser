using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZpSolutionWbBuyoutsParser.Parser;
using ZpSolutionWbBuyoutsParser.Mongo;
using MongoDB.Bson;
using MongoDB.Driver;
using ZpSolutionWbBuyoutsParser.WbStorage;
using ZpSolutionWbBuyoutsParser.Models.Bson.WB;
using ZpSolutionWbBuyoutsParser.ZennoPoster;
using ZpSolutionWbBuyoutsParser.Models.Json;

namespace ZpSolutionWbBuyoutsParser.OrdersManager
{
    internal class ArchiveOrdersManager : IOrdersManager
    {
        private readonly WbAccountOrdersParser _orderParser;
        private readonly WbProductsCollection _productCollection;
        private readonly IOrderArchiveStatusConverter _archiveOrderStatusConverter;
        private readonly ZennoPosterProfile _zennoPosterProfile;
        private readonly DateTime _minDateForWrite;

        public ArchiveOrdersManager(WbAccountOrdersParser ordersParser, ZennoPosterProfile zennoPosterProfile, IOrderArchiveStatusConverter orderArchiveStatusConverter)
        {
            _orderParser = ordersParser;
            _zennoPosterProfile = zennoPosterProfile;
            _archiveOrderStatusConverter = orderArchiveStatusConverter;
            _productCollection = new WbProductsCollection();
            _minDateForWrite = DateTime.Now.AddDays(-90);
        }

        public void UpdateOrdersData()
        {
            var archiveProducts = _orderParser.GetArchiveProducts();
            foreach (var archiveProduct in archiveProducts)
            {
                string currentStatus = _archiveOrderStatusConverter.GetDbFormatStatus(archiveProduct.Status);
                var product = _productCollection.FindProduct(archiveProduct.RId);
                if(product != null)
                {
                    if(product.Status != currentStatus)
                    {
                        product.Status = currentStatus;
                        _productCollection.Replace(product);
                    }
                    else
                    {
                        _productCollection.UpdateCheckDate(product.Id);
                    }
                }
                else
                {
                    AddNewProduct(archiveProduct);
                }
            }
        }

        private void AddNewProduct(ArchiveProductModel archiveProduct)
        {
            if (ProductSuitableForDb(archiveProduct))
            {
                var newProduct = CreateNewArchiveProduct(archiveProduct);
                _productCollection.Insert(newProduct);
            }
        }

        private ProductModel CreateNewArchiveProduct(ArchiveProductModel archiveProduct)
        {
            string currentStatus = _archiveOrderStatusConverter.GetDbFormatStatus(archiveProduct.Status);
            ProductModel productModel = new ProductModel
            {
                Address = null,
                IsActive = true,
                Brand = archiveProduct.Brand,
                BuyoutsDate = archiveProduct.OrderDate,
                CancelDate = null,
                Code = string.Empty,
                LastCheck = DateTime.Now.AddHours(3),
                LastUpdate = DateTime.Now.AddHours(3),
                OrderDate = archiveProduct.OrderDate.AddHours(3),
                Title = archiveProduct.Name,
                Price = archiveProduct.Price,
                ProductId = archiveProduct.ProductId,
                ReciveDate = string.Empty,
                ReviewDate = null,
                ReviewExists = false,
                RID = archiveProduct.RId,
                Session = _zennoPosterProfile.SessionName,
                Status = currentStatus
            };
            return productModel;
        }

        private bool ProductSuitableForDb(ArchiveProductModel archiveProduct)
        {
            if(archiveProduct.OrderDate > _minDateForWrite)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
