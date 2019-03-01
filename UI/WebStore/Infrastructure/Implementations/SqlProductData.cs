using Microsoft.EntityFrameworkCore;
using MyWebStore.DALNew.Context;
using MyWebStore.DomainEntities.Entities;
using MyWebStore.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebStore.Infrastructure.Implementations
{
    public class SqlProductData : IProductData
    {
        private readonly MyWebStoreContext _db;

        public SqlProductData(MyWebStoreContext db) => _db = db;


        public IEnumerable<Brand> GetBrands()
        {
            return _db.Brands.AsEnumerable();
        }

        public Product GetProductById(int id)
        {
            return _db.Products
                 .Include(p => p.Brand)
                .Include(p => p.Section)
                .FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Product> GetProducts(ProductFilter filter)
        {
            if (filter is null)
                return _db.Products
                        .Include(p => p.Brand)
                        .Include(p => p.Section)
                        .AsEnumerable();

            IQueryable<Product> result = _db.Products
                 .Include(p => p.Brand)
                 .Include(p => p.Section);

            if (filter.BrandId != null)
                return result.Where(p => p.BrandId == filter.BrandId);
            if (filter.SectionId != null)
                return result.Where(p => p.SectionId == filter.SectionId);

            return result.AsEnumerable();
        }

        public int GetProductsBrandCount(int brandId)
        {
            return _db.Products.Count(p => p.BrandId == brandId);
        }

        public IEnumerable<Section> GetSections()
        {
            return _db.Sections.AsEnumerable();
        }
    }
}
