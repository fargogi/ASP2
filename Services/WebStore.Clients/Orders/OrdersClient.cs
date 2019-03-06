using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;
using MyWebStore.DomainNew.DTO.Order;
using MyWebStore.DomainNew.Entities;
using WebStore.Clients.Base;
using WebStore.Interfaces;

namespace WebStore.Clients.Orders
{
    public class OrdersClient : BaseClient, IOrderService
    {
        public OrdersClient(IConfiguration configuration) : base(configuration) => ServiceAddress = "api/orders";

        public OrderDTO CreateOrder(CreateOrderModel Model, string UserName)
        {
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
