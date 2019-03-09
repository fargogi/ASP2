using MyWebStore.DomainNew.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebStore.DomainNew.DTO.Order
{
    public class OrderItemDTO : BaseEntity
    {
        public decimal Price { get; set; }

        public int Quantity { get; set; }


    }
}
