using Microsoft.EntityFrameworkCore;
using MyWebStore.DAL;
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
    public class SqlProductData : IProductData
    {
        private readonly MyWebStoreContext _db;

        public SqlProductData(MyWebStoreContext db) => _db = db;

        public Brand GetBrandById(int id) => _db.Brands.FirstOrDefault(b => b.Id == id);

        public IEnumerable<Brand> GetBrands()
        {
            return _db.Brands.AsEnumerable();
        }

        public ProductDTO GetProductById(int id)
        {
            return _db.Products
                 .Include(p => p.Brand)
                .Include(p => p.Section)
                .FirstOrDefault(p => p.Id == id).Map();
        }

        public PagedProductDTO GetProducts(ProductFilter filter)
        {
            IQueryable<Product> query = _db.Products
                        .Include(p => p.Brand)
                        .Include(p => p.Section);

            if (filter is null)
                return new PagedProductDTO
                {
                    Products = query
                    .AsEnumerable()
                    .Select(ProductDTO2Product.Map),

                    TotalCount = query.Count()
                };

            IQueryable<Product> result = _db.Products
                 .Include(p => p.Brand)
                 .Include(p => p.Section);

            if (filter.BrandId != null)
                result = result.Where(p =>
                  p.BrandId == filter.BrandId);
            if (filter.SectionId != null)
                result = result.Where(p => p.SectionId == filter.SectionId);

            var total_count = result.Count();

            if (filter.PageSize != null)
            {
                result = result
                    .Skip(filter.Page - 1 * (int)filter.PageSize)
                    .Take((int)filter.PageSize);
            }

            var model = new PagedProductDTO
            {
                TotalCount = total_count,
                Products = result.AsEnumerable().Select(ProductDTO2Product.Map)
            };

            return model;
        }

        public int GetProductsBrandCount(int brandId)
        {
            return _db.Products.Count(p => p.BrandId == brandId);
        }

        public Section GetSectionById(int id) => _db.Sections.FirstOrDefault(s => s.Id == id);

        public IEnumerable<Section> GetSections()
        {
            return _db.Sections.AsEnumerable();
        }
    }
}
