using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebStore.DomainNew.DTO.Order;
using MyWebStore.DomainNew.Entities;
using WebStore.Interfaces;

namespace WebStore.ServiceHosting.Controllers
{
    [ApiController, Route("api/[controller]"), Produces("application/json")]
    public class OrdersController : ControllerBase, IOrderService
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("{UserName?}")]
        public OrderDTO CreateOrder([FromBody] CreateOrderModel Model, string UserName)
        {
            return _orderService.CreateOrder(Model, UserName);
        }

        [HttpGet("{id}"), ActionName("Get")]
        public OrderDTO GetOrderById(int id)
        {
            return _orderService.GetOrderById(id);
        }

        [HttpGet("user/{UserName}")]
        public IEnumerable<OrderDTO> GetUserOrders(string UserName)
        {
            return _orderService.GetUserOrders(UserName);
        }
    }
}