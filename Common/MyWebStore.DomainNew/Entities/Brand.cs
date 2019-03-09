using MyWebStore.DomainNew.Entities.Base;
using MyWebStore.DomainNew.Entities.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebStore.DomainNew.Entities
{
    /// <summary>Бренд</summary>
    public class Brand : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }
       
        public virtual ICollection<Product> Products { get; set; }
    }
}
