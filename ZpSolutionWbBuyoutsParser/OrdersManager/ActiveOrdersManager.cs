using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZpSolutionWbBuyoutsParser.Models.Bson.WB;
using ZpSolutionWbBuyoutsParser.Models.Json;
using ZpSolutionWbBuyoutsParser.Mongo;
using ZpSolutionWbBuyoutsParser.Parser;
using ZpSolutionWbBuyoutsParser.WbStorage;
using ZpSolutionWbBuyoutsParser.ZennoPoster;

namespace ZpSolutionWbBuyoutsParser.OrdersManager
{
    internal class ActiveOrdersManager : IOrdersManager
    {
        private readonly WbAccountOrdersParser _orderParser;
        private readonly WbProductsCollection _productCollection;
        private readonly ZennoPosterProfile _zennoPosterProfile;
        private readonly IOrderActiveStatusConverter _orderActiveStatusConverter;

        public ActiveOrdersManager(WbAccountOrdersParser ordersParser, ZennoPosterProfile zennoPosterProfile, IOrderActiveStatusConverter orderActiveStatusConverter)
        {
            _orderParser = ordersParser;
            _zennoPosterProfile = zennoPosterProfile;
            _orderActiveStatusConverter = orderActiveStatusConverter;
            _productCollection = new WbProductsCollection();
        }

        public void UpdateOrdersData()
        {
            var orders = _orderParser.GetActiveOrders();
            foreach (var currentOrder in orders.ActiveOrders)
            {
                var dbProduct = _productCollection.FindProduct(currentOrder.RId);
                if(dbProduct != null)
                {
                    dbProduct.Status = _orderActiveStatusConverter.GetDbFormatStatus(currentOrder.IsReadyToReceiveToday);
                    dbProduct.Code = orders.PrivateCode;
                    dbProduct.ReciveDate = currentOrder.ExpireDate;
                    _productCollection.Replace(dbProduct);
                }
                else
                {
                    var newProduct = CreateNewActiveProduct(currentOrder, orders);
                    _productCollection.Insert(newProduct);
                }
            }
        }

        private DeliveryPointInfoModel ChooseDeliveryPoint(ActiveOrderModel activeOrder, Dictionary<string, DeliveryPointInfoModel> deliveryPoints)
        {
            if(deliveryPoints.ContainsKey(activeOrder.LocationId))
            {
                return deliveryPoints[activeOrder.LocationId];
            }
            else
            {
                return new DeliveryPointInfoModel();
            }
        }

        private ProductModel CreateNewActiveProduct(ActiveOrderModel activeOrder, ActiveOrdersStorageModel ordersStore)
        {
            string currentStatus = _orderActiveStatusConverter.GetDbFormatStatus(activeOrder.IsReadyToReceiveToday);
            string address = ChooseDeliveryPoint(activeOrder, ordersStore.DeliveryPoints).Address;
            ProductModel productModel = new ProductModel
            {
                Address = address,
                IsActive = true,
                Brand = activeOrder.Brand,
                BuyoutsDate = activeOrder.OrderDate,
                CancelDate = null,
                Code = ordersStore.PrivateCode,
                LastCheck = DateTime.Now.AddHours(3),
                LastUpdate = DateTime.Now.AddHours(3),
                OrderDate = activeOrder.OrderDate.AddHours(3),
                Title = activeOrder.Title,
                Price = activeOrder.Price,
                ProductId = activeOrder.ProductId,
                ReciveDate = activeOrder.ExpireDate,
                ReviewDate = null,
                ReviewExists = false,
                RID = activeOrder.RId,
                Session = _zennoPosterProfile.SessionName,
                Status = currentStatus
            };
            return productModel;
        }
    }
}
