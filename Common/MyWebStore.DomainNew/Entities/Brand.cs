using MyWebStore.DomainEntities.Entities.Base;
using MyWebStore.DomainEntities.Entities.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebStore.DomainEntities.Entities
{
    /// <summary>Бренд</summary>
    public class Brand : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }
       
        public virtual ICollection<Product> Products { get; set; }
    }
}
