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

namespace WebStore.Services
{
    public class SqlProductData : IProductData
    {
        private readonly MyWebStoreContext _db;

        public SqlProductData(MyWebStoreContext db) => _db = db;


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

        public IEnumerable<ProductDTO> GetProducts(ProductFilter filter)
        {
            if (filter is null)
                return _db.Products
                        .Include(p => p.Brand)
                        .Include(p => p.Section)
                        .AsEnumerable()
                        .Select(ProductDTO2Product.Map);

            IQueryable<Product> result = _db.Products
                 .Include(p => p.Brand)
                 .Include(p => p.Section);

            if (filter.BrandId != null)
                return result.Where(p => 
                p.BrandId == filter.BrandId)
                    .Select(ProductDTO2Product.Map);
            if (filter.SectionId != null)
                return result.Where(p => p.SectionId == filter.SectionId)
                    .Select(ProductDTO2Product.Map);

            return result.Select(ProductDTO2Product.Map).AsEnumerable();
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
