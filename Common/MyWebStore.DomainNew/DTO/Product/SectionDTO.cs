using MyWebStore.DomainNew.Entities.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebStore.DomainNew.DTO.Product
{
    public class SectionDTO : INamedEntity
    {
        public string Name { get; set; }

        public int Id { get; set; }
    }
}
