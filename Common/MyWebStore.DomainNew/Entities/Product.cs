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
    /// <summary>Сущность товар</summary>
    public class Product: NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        /// <summary>Секция, которой принадлежит товар</summary>
        public int SectionId { get; set; }

        /// <summary>Бренд</summary>
        public int? BrandId { get; set; }

        /// <summary>URL картинки товара</summary>
        public string ImageUrl { get; set; }

        /// <summary>Цена товара</summary>
        [Column(TypeName="decimal(18,2)")]
        public decimal Price { get; set; }

        [ForeignKey("SectionId")]
        public virtual Section Section { get; set; }

        [ForeignKey("BrandId")]
        public virtual Brand Brand { get; set; }
    }
}
