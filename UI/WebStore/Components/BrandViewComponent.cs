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
    public class BrandViewComponent : ViewComponent
    {
        private readonly IProductData _productData;

        public BrandViewComponent(IProductData productData)
        {
            _productData = productData;
        }

        public async Task<IViewComponentResult> InvokeAsync(string BrandId)
        {
            var brand_id = int.TryParse(BrandId, out var id) ? id : (int?)null;

            var brands = GetBrands();
            return View(new BrandCompleteViewModel
            {
                Brands = GetBrands(),
                CurrentBrandId = id
            });
        }

        private List<BrandViewModel> GetBrands()
        {
            var brands = _productData.GetBrands();
            var brand_views = brands.Select(brand => new BrandViewModel
            {
                Id = brand.Id,
                Name = brand.Name,
                Order = brand.Order,
                ProductsCount = _productData.GetProductsBrandCount(brand.Id)
            }).ToList();

            brand_views.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));
            return brand_views;

        }

    }
}
