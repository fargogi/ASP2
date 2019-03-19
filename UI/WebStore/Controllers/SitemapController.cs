using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyWebStore.DomainNew.Entities;
using SimpleMvcSitemap;
using WebStore.Interfaces;

namespace MyWebStore.Controllers
{
    public class SitemapController : Controller
    {
        private readonly IProductData _productData;

        public SitemapController(IProductData productData) => _productData = productData;

        public IActionResult Index()
        {
            var nodes = new List<SitemapNode>
            {
                new SitemapNode(Url.Action("Index", "Home")),
                new SitemapNode(Url.Action("Shop", "Catalog")),
                new SitemapNode(Url.Action("BlogSingle", "Home")),
                new SitemapNode(Url.Action("Blog", "Home")),
                new SitemapNode(Url.Action("ContactUs", "Home")),
            };

            foreach (var section in _productData.GetSections())
            {
                if (section.ParentSection == null)
                    nodes.Add(new SitemapNode(Url.Action("Shop", "Catalog", new { SectionId = section.Id })));
            }

            foreach (var brand in _productData.GetBrands())
            {
                    nodes.Add(new SitemapNode(Url.Action("Shop", "Catalog", new { BrandId = brand.Id })));
            }

            foreach (var product in _productData.GetProducts(new ProductFilter()))
            {
                nodes.Add(new SitemapNode(Url.Action("ProductDetails", "Catalog", new { Id = product.Id })));
            }

            return new SitemapProvider().CreateSitemap(new SitemapModel(nodes));
        }
    }
}