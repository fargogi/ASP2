using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebStore.DomainNew.Entities;
using MyWebStore.DomainNew.DTO;
using WebStore.Interfaces;

namespace WebStore.ServiceHosting.Controllers
{
    [ApiController, Route("api/[controller]")]

    public class ProductsController : ControllerBase, IProductData
    {
        private readonly IProductData _productData;

        public ProductsController(IProductData productData)
        {
            _productData = productData;
        }

        [HttpGet("brands")]
        public IEnumerable<Brand> GetBrands()
        {
            return _productData.GetBrands();
        }

        [HttpGet("{id}"), ActionName("Get")]
        public ProductDTO GetProductById(int id)
        {
            return _productData.GetProductById(id);
        }

        [HttpPost, ActionName("Post")]
        public IEnumerable<ProductDTO> GetProducts([FromBody] ProductFilter filter = null)
        {
            return _productData.GetProducts(filter);
        }

        public int GetProductsBrandCount(int brandId)
        {
            return _productData.GetProductsBrandCount(brandId);
        }

        [HttpGet("sections")]
        public IEnumerable<Section> GetSections()
        {
            return _productData.GetSections();
        }
    }
}