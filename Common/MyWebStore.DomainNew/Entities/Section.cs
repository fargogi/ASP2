using MyWebStore.DomainEntities.Entities.Base;
using MyWebStore.DomainEntities.Entities.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebStore.DomainEntities.Entities
{
    /// <summary>Секция товаров</summary>
    public class Section : NamedEntity, IOrderedEntity
    {
        public int? ParentId { get; set; }
        public int Order { get; set; }

        [ForeignKey("ParentId")]
        public virtual Section ParentSection { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
