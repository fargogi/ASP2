using MyWebStore.DomainNew.Entities.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebStore.DomainNew.DTO
{
    public class ProductDTO : INamedEntity, IOrderedEntity
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int Order { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public BrandDTO Brand { get; set; }
    }
}
