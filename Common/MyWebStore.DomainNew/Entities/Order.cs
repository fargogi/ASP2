using MyWebStore.DomainEntities.Entities;
using MyWebStore.DomainEntities.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebStore.DomainNew.Entities
{
    /// <summary>Заказ</summary>
    public class Order : NamedEntity
    {
        public virtual User User { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public DateTime Date { get; set; }

        /// <summary>Элементы заказа</summary>
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
