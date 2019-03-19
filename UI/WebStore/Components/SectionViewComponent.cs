using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces;
using MyWebStore.DomainNew.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebStore.DomainNew.ViewModels.Product;

namespace MyWebStore.Components
{
    public class SectionViewComponent: ViewComponent
    {
        private readonly IProductData _productData;

        public SectionViewComponent(IProductData productData)
        {
            _productData = productData;
        }

        public async Task<IViewComponentResult> InvokeAsync(string sectionId)
        {
            var section_id = int.TryParse(sectionId, out var id) ? id : (int?)null;

            var sections = GetSections(section_id, out var parent_section_id);
            return View(new SectionCompleteViewModel
            {
                Sections = sections,
                CurrentSectionId = section_id,
                CurrentParentSectionId = parent_section_id
            });
        }

        private List<SectionViewModel> GetSections(int? sectionId, out int? parentSectionId)
        {
            parentSectionId = null;
            var sections = _productData.GetSections();
            var parent_sections = sections.Where(section => section.ParentId is null).ToArray();

            var parent_section_views = parent_sections
                .Select(parent_section => new SectionViewModel
                {
                    Id = parent_section.Id,
                    Name = parent_section.Name,
                    Order = parent_section.Order
                }).ToList();

            foreach (var parent_section_view in parent_section_views)
            {
                var children_sections = sections.Where(section => section.ParentId == parent_section_view.Id);
                foreach (var child_section in children_sections)
                {
                    if (child_section.Id == sectionId)
                        parentSectionId = parent_section_view.Id;

                    parent_section_view.ChildSection.Add(new SectionViewModel
                    {
                        Id = child_section.Id,
                        Name = child_section.Name,
                        Order = child_section.Order,
                        ParentSection = parent_section_view
                    });
                }
                parent_section_view.ChildSection.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));
            }
            parent_section_views.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));
            return parent_section_views;
        }
    }
}
