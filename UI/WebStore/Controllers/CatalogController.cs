using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyWebStore.DomainNew.Entities;
using WebStore.Interfaces;
using MyWebStore.DomainNew.ViewModels;
using Microsoft.Extensions.Configuration;

namespace MyWebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData _productData;
        private readonly IConfiguration _configuration;

        public CatalogController(IProductData productData, IConfiguration configuration)
        {
            _productData = productData;
            _configuration = configuration;
        }


        public IActionResult Shop(int? sectionId, int? brandId, int page = 1)
        {
            var page_size = int.Parse(_configuration["PageSize"]);

            var products = _productData.GetProducts(new ProductFilter
            {
                BrandId = brandId,
                SectionId = sectionId,
                Page = page,
                PageSize = page_size
            });

            var model = new CatalogViewModel
            {
                BrandId = brandId,
                SectionId = sectionId,
                Products = products.Products.Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    ImageUrl = p.ImageUrl,
                    Name = p.Name,
                    Order = p.Order,
                    Price = p.Price,
                    Brand = p.Brand?.Name ?? String.Empty
                }).OrderBy(p => p.Order).ToArray(),
                PageViewModel = new PageViewModel
                {
                    PageSize = page_size,
                    PageNumber = page,
                    TotalItems = products.TotalCount
                }
            };

            return View(model);
        }


        public IActionResult ProductDetails(int id)
        {
            var product = _productData.GetProductById(id);

            if (product is null)
                return NotFound();


            return View(new ProductViewModel
            {
                Id = product.Id,
                ImageUrl = product.ImageUrl,
                Name = product.Name,
                Order = product.Order,
                Price = product.Price,
                Brand = product.Brand?.Name ?? String.Empty
            });
        }

        public IActionResult GetFilteredItems(int? sectionId, int? brandId, int page=1)
        {
            var products = GetProducts(sectionId, brandId, page);
            return PartialView("Partial/_ProductItems", products);
        }

        private IEnumerable<ProductViewModel> GetProducts(int? sectionId, int? brandId, int page)
        {
            var products = _productData.GetProducts(new ProductFilter
            {
                SectionId = sectionId,
                BrandId = brandId,
                Page = page,
                PageSize = int.Parse(_configuration["PageSize"])
            });

            return products.Products.Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Brand = p.Brand?.Name ?? string.Empty,
                ImageUrl = p.ImageUrl,
                Order = p.Order
            }).ToArray();
        }
    }
}