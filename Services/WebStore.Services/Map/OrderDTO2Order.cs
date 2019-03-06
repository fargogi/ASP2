using MyWebStore.DomainNew.DTO.Order;
using MyWebStore.DomainNew.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebStore.Services.Map
{
    public static class OrderDTO2Order
    {
        public static OrderDTO Map(this Order order) =>
            new OrderDTO
            {
                Id = order.Id,
                Name = order.Name,
                Date = order.Date,
                Address = order.Address,
                Phone = order.Phone,
                Items = order.OrderItems?.Select(item => new OrderItemDTO
                {
                    Id=order.Id,
                    Quantity=order.OrderItems.Count
                })
            };
    }
}
