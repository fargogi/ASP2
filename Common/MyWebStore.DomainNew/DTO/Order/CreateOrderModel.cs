using MyWebStore.DomainNew.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebStore.DomainNew.DTO.Order
{
   public class CreateOrderModel
    {
        public OrderViewModel OrderViewModel { get; set; }

        public List<OrderItemDTO> Items { get; set; }
    }
}
