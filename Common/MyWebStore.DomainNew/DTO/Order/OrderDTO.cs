using MyWebStore.DomainEntities.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebStore.DomainNew.DTO.Order
{
    public class OrderDTO : NamedEntity
    {
        public string Phone { get; set; }

        public string Address { get; set; }

        public DateTime Date { get; set; }

        public IEnumerable<OrderItemDTO> Items { get; set; }
    }
}
