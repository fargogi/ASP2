﻿using MyWebStore.DomainNew.Entities;
using MyWebStore.DomainNew.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Services.Map
{
    public static class ProductDTO2Product
    {
        public static ProductDTO Map(this Product product)
        {
            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Order = product.Order,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Brand = product.Brand is null ? null: new BrandDTO
                {
                    Id = product.Brand.Id,
                    Name = product.Brand.Name
                }
            };
        }
    }
}
