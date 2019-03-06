using MyWebStore.DomainNew.DTO.Order;
using MyWebStore.DomainNew.Entities;
using MyWebStore.DomainNew.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<OrderDTO> GetUserOrders(string UserName);

        OrderDTO GetOrderById(int id);

        OrderDTO CreateOrder(CreateOrderModel Model, string UserName);
    }
}
