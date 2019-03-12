using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyWebStore.DomainNew.DTO.Order;
using MyWebStore.DomainNew.Entities;
using WebStore.Clients.Base;
using WebStore.Interfaces;

namespace WebStore.Clients.Orders
{
    public class OrdersClient : BaseClient, IOrderService
    {
        private readonly ILogger _logger;

        public OrdersClient(IConfiguration configuration, ILogger logger) 
            : base(configuration)
        {
            ServiceAddress = "api/orders";
            _logger = logger;
        }

        public OrderDTO CreateOrder(CreateOrderModel Model, string UserName)
        {
            _logger.LogInformation($"Создание заказа для {UserName}");
            var response = Post($"{ServiceAddress}/{UserName}", Model);
            return response.Content.ReadAsAsync<OrderDTO>().Result;
        }

        public OrderDTO GetOrderById(int id)
        {
            return Get<OrderDTO>($"{ServiceAddress}/{id}");
        }

        public IEnumerable<OrderDTO> GetUserOrders(string UserName)
        {
            return Get<List<OrderDTO>>($"{ServiceAddress}/user/{UserName}");
        }
    }
}
