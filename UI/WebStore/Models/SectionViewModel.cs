using MyWebStore.DomainEntities.Entities.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MyWebStore.Models
{
    public class SectionViewModel : INamedEntity, IOrderedEntity
    {
        public string Name { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public int Order { get; set; }

        public List<SectionViewModel> ChildSection { get; set; } = new List<SectionViewModel>();
        public SectionViewModel ParentSection { get; set; }
    }
}
