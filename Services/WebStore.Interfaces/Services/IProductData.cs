using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebStore.DomainNew.Entities;
using MyWebStore.DomainNew.DTO;
using MyWebStore.DomainNew.DTO.Product;

namespace WebStore.Interfaces
{
    public interface IProductData
    {
        /// <summary>Бренды</summary>
        /// <returns>Возвращает бренды</returns>
        IEnumerable<Brand> GetBrands();

        Brand GetBrandById(int id);

        /// <summary>Секции</summary>
        /// <returns>Возвращает секции</returns>
        IEnumerable<Section> GetSections();

        Section GetSectionById(int id);

        /// <summary>Товары</summary>
        /// <param name="filter">Фильтрация товаров</param>
        /// <returns>Возвращает список товаров</returns>
        PagedProductDTO GetProducts(ProductFilter filter = null);

        /// <summary>Метод возвращающий количество товаров определенного бренда</summary>
        /// <param name="brandId">Идентификатор бренда</param>
        /// <returns>Возвращает количество товаров</returns>
        int GetProductsBrandCount(int brandId);

        ProductDTO GetProductById(int id);
    }
}
