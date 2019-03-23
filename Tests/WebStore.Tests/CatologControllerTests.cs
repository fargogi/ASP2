using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyWebStore.Controllers;
using MyWebStore.DomainNew.DTO;
using MyWebStore.DomainNew.DTO.Product;
using MyWebStore.DomainNew.Entities;
using MyWebStore.DomainNew.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebStore.Interfaces;
using Xunit.Sdk;

using Assert = Xunit.Assert;

namespace WebStore.Tests
{
    [TestClass]
    public class CatologControllerTests
    {
        private const int EXPECTED_ID = 10;
        private const string EXPECTED_NAME = "Product_Name";
        private const int EXPECTED_ORDER = 1;
        private const int EXPECTED_PRICE = 11;
        private const string EXPECTED_IMAGE_URL = "image.jpg";
        private const int EXPECTED_BRAND_ID = 1;
        private const string EXPECTED_BRAND_NAME = "Brand_Name";


        [TestMethod]
        public void ProductDetails_Returns_View_With_Correct_Item()
        {
            var product_data_mock = new Mock<IProductData>();

            product_data_mock
                .Setup(p => p.GetProductById(It.IsAny<int>()))
                .Returns<int>(id => new ProductDTO
                {
                    Id = EXPECTED_ID,
                    Name = EXPECTED_NAME,
                    Order = EXPECTED_ORDER,
                    Price = EXPECTED_PRICE,
                    ImageUrl = EXPECTED_IMAGE_URL,
                    Brand = new BrandDTO
                    {
                        Id = EXPECTED_BRAND_ID,
                        Name = EXPECTED_BRAND_NAME
                    }
                });

            var controller = new CatalogController(product_data_mock.Object, new Mock<IConfiguration>().Object);

            var result = controller.ProductDetails(EXPECTED_ID);

            var view_result = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<ProductViewModel>(view_result.ViewData.Model);

            Assert.Equal(EXPECTED_ID, model.Id);
            Assert.Equal(EXPECTED_NAME, model.Name);
            Assert.Equal(EXPECTED_ORDER, model.Order);
            Assert.Equal(EXPECTED_PRICE, model.Price);
            Assert.Equal(EXPECTED_IMAGE_URL, model.ImageUrl);
            Assert.Equal(EXPECTED_BRAND_NAME, model.Brand);
        }

        [TestMethod]
        public void ProductDetails_Product_Not_Found()
        {
            var product_data_mock = new Mock<IProductData>();

            product_data_mock
                .Setup(p => p.GetProductById(It.IsAny<int>()))
                .Returns((ProductDTO)null);

            var controller = new CatalogController(product_data_mock.Object, new Mock<IConfiguration>().Object);

            var result = controller.ProductDetails(-1);

            var not_found_result = Assert.IsType<NotFoundResult>(result);
        }

        [TestMethod]
        public void Shop_Method_Returns_Correct_View()
        {
            const int EXPECTED_SECTION_ID = 10;
            const int EXPECTED_BRAND_ID = 5;

            var product_data_mock = new Mock<IProductData>();
            product_data_mock
                .Setup(p => p.GetProducts(It.IsAny<ProductFilter>()))
                .Returns<ProductFilter>(filter => new PagedProductDTO
                {
                        Products = new[]
                    {
                        new ProductDTO
                        {
                            Id = 1,
                            Name="Product 1",
                            Order = 1,
                            Price = 10,
                            ImageUrl = "image1.jpg",
                            Brand = new BrandDTO
                            {
                                Id=1,
                                Name = "Brand 1"
                            }

                        },
                        new ProductDTO
                        {
                            Id = 2,
                            Name="Product 2",
                            Order = 2,
                            Price = 20,
                            ImageUrl = "image2.jpg",
                            Brand = new BrandDTO
                            {
                                Id=1,
                                Name = "Brand 1"
                            }

                        }
                    }
                });

            var controller = new CatalogController(product_data_mock.Object, new Mock<IConfiguration>().Object);

            var result = controller.Shop(EXPECTED_SECTION_ID, EXPECTED_BRAND_ID);

            var view_result = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<CatalogViewModel>(view_result.ViewData.Model);

            Assert.Equal(2, model.Products.Count());
            Assert.Equal(EXPECTED_BRAND_ID, model.BrandId);
            Assert.Equal(EXPECTED_SECTION_ID, model.SectionId);
        }
    }
}
