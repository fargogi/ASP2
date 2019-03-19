﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;
using MyWebStore.DomainNew.Entities;
using MyWebStore.DomainNew.DTO;
using WebStore.Clients.Base;
using WebStore.Interfaces;

namespace WebStore.Clients.Products
{
    public class ProductsClient : BaseClient, IProductData
    {
        public ProductsClient(IConfiguration configuration) : base(configuration)
        {
            ServiceAddress = "api/products";
        }

        public Brand GetBrandById(int id) => Get<Brand>($"{ServiceAddress}/brands/{id}");

        public IEnumerable<Brand> GetBrands()
        {
            return Get<List<Brand>>($"{ServiceAddress}/brands");
        }

        public ProductDTO GetProductById(int id)
        {
            return Get<ProductDTO>($"{ServiceAddress}/{id}");
        }

        public IEnumerable<ProductDTO> GetProducts(ProductFilter filter = null)
        {
            var response = Post(ServiceAddress, filter);
            return response.Content.ReadAsAsync<IEnumerable<ProductDTO>>().Result;
        }

        public int GetProductsBrandCount(int brandId)
        {
            //Переделать костыль
            return 1;
        }

        public Section GetSectionById(int id) => Get<Section>($"{ServiceAddress}/sections/{id}");

        public IEnumerable<Section> GetSections() => Get<List<Section>>($"{ServiceAddress}/sections");
    }
}
