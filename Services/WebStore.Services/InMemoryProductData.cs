using MyWebStore.Data;
using MyWebStore.DomainNew.Entities;
using WebStore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebStore.DomainNew.DTO;
using WebStore.Services.Map;
using MyWebStore.DomainNew.DTO.Product;

namespace WebStore.Services
{
    class InMemoryProductData : IProductData
    {
        public IEnumerable<Brand> GetBrands() => TestData.Brands;


        public IEnumerable<Section> GetSections() => TestData.Sections;

        public PagedProductDTO GetProducts(ProductFilter filter)
        {
            var products = TestData.Products;
            if (filter.SectionId.HasValue)
            {
                products = products.Where(p => p.SectionId.Equals(filter.SectionId)).ToList();
            }
            if (filter.BrandId.HasValue)
            {
                products = products.Where(p => p.BrandId.HasValue && 
                p.BrandId.Value.Equals(filter.BrandId.Value)).ToList();
            }

            return new PagedProductDTO {Products = products.Select(ProductDTO2Product.Map)};
        }

        public int GetProductsBrandCount(int brandId)
        {
            return TestData.Products.Count(p => p.BrandId == brandId);
        }

        public ProductDTO GetProductById(int id)
        {
            return TestData.Products.FirstOrDefault(p => p.Id == id).Map();
        }

        public Brand GetBrandById(int id)
        {
            throw new NotImplementedException();
        }

        public Section GetSectionById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
