using Microsoft.AspNetCore.Mvc;
using MyWebStore.DomainNew.ViewModels.BreadCrumbs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Interfaces;

namespace MyWebStore.Components
{
    public class BreadCrumbsViewComponent : ViewComponent
    {
        private readonly IProductData _productData;

        public BreadCrumbsViewComponent(IProductData productData) => _productData = productData;

        public IViewComponentResult Invoke(BreadCrumbType type, int id, BreadCrumbType fromType)
        {
            if (!Enum.IsDefined(typeof(BreadCrumbType), type))
                throw new InvalidEnumArgumentException(nameof(type), (int)type, typeof(BreadCrumbType));
            if (!Enum.IsDefined(typeof(BreadCrumbType), fromType))
                throw new InvalidEnumArgumentException(nameof(fromType), (int)fromType, typeof(BreadCrumbType));

            switch (type)
            {

                case BreadCrumbType.Section:
                    var section = _productData.GetSectionById(id);
                    return View(new[]
                    {
                        new BreadCrumbsViewModel
                        {
                            BreadCrumbType = type,
                            Id = section.Id.ToString(),
                            Name = section.Name
                        }

                    });
                case BreadCrumbType.Brand:
                    var brand = _productData.GetBrandById(id);
                    return View(new[]
                   {
                        new BreadCrumbsViewModel
                        {
                            BreadCrumbType = type,
                            Id = brand.Id.ToString(),
                            Name = brand.Name
                        }

                    });
                case BreadCrumbType.Item:
                    return View(GetItemBreadCrumbs(id, fromType, type));
            }
            return View(new BreadCrumbsViewModel[0]);
        }

        private IEnumerable<BreadCrumbsViewModel> GetItemBreadCrumbs(int id, BreadCrumbType fromType, BreadCrumbType type)
        {
            var item = _productData.GetProductById(id);

            var crumbs = new List<BreadCrumbsViewModel>();

            if (fromType == BreadCrumbType.Section)
                crumbs.Add(
                    new BreadCrumbsViewModel
                    {
                        BreadCrumbType = fromType,
                        Id = item.Section.Id.ToString(),
                        Name = item.Section.Name
                    });
            else
                crumbs.Add(
                   new BreadCrumbsViewModel
                   {
                       BreadCrumbType = fromType,
                       Id = item.Brand.Id.ToString(),
                       Name = item.Brand.Name
                   });

            crumbs.Add(
                   new BreadCrumbsViewModel
                   {
                       BreadCrumbType = type,
                       Id = item.Id.ToString(),
                       Name = item.Name
                   });
            return crumbs;
        }
    }
}
