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
    internal class ArchiveOrdersManager
    {
        private readonly WbAccountOrdersParser _orderParser;
        private readonly WbProductsCollection _productCollection;
        private readonly IOrderStatusConverter _archiveOrderStatusConverter;
        private readonly ZennoPosterProfile _zennoPosterProfile;

        public ArchiveOrdersManager(WbAccountOrdersParser ordersParser, ZennoPosterProfile zennoPosterProfile)
        {
            _orderParser = ordersParser;
            _zennoPosterProfile = zennoPosterProfile;
            _productCollection = new WbProductsCollection();
            _archiveOrderStatusConverter = new ArchiveOrderStatusConverterV1();
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
                    CreateNewArchiveProduct(archiveProduct);
                }
            }
        }

        private void CreateNewArchiveProduct(ArchiveProductModel archiveProduct)
        {
            if(ProductSuitableForDb(archiveProduct))
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
                    LastCheck = DateTime.Now,
                    LastUpdate = DateTime.Now,
                    OrderDate = archiveProduct.OrderDate,
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
                _productCollection.Insert(productModel);
            }
        }

        private bool ProductSuitableForDb(ArchiveProductModel archiveProduct)
        {
            DateTime writeDate = DateTime.Now.AddDays(-90);
            if(archiveProduct.OrderDate > writeDate)
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
