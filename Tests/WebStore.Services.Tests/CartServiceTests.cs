using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyWebStore.DomainNew.ViewModels;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using Assert = Xunit.Assert;
using WebStore.Interfaces;
using Moq;
using WebStore.Interfaces.Services;
using MyWebStore.DomainNew.DTO;
using MyWebStore.DomainNew.Entities;

namespace WebStore.Services.Tests
{
    [TestClass]
    public class CartServiceTests
    {
        [TestMethod]
        public void Cart_Class_Items_Count_Returns_Correct_Quantity()
        {
            var cart = new Cart
            {
                Items = new List<CartItem>
                {
                    new CartItem
                    {
                        ProductId = 1,
                        Quantity = 1
                    },
                    new CartItem
                    {
                        ProductId = 2,
                        Quantity = 5
                    }
                }
            };

            var expected_count = cart.Items.Sum(i => i.Quantity);

            var actual_count = cart.ItemsCount;

            Assert.Equal(expected_count, actual_count);
        }

        [TestMethod]
        public void CartViewModel_Returns_Correct_ItemsCount()
        {
            var cart_view_model = new CartViewModel
            {
                Items = new Dictionary<ProductViewModel, int>
                {
                    {new ProductViewModel{Id=1, Name="Product 1", Price = 5m}, 1 },
                    {new ProductViewModel{Id=2, Name="Product 2", Price = 10m}, 2 }
                }
            };

            var expected_count = cart_view_model.Items.Sum(i => i.Value);

            var actual_count = cart_view_model.ItemsCount;

            Assert.Equal(expected_count, actual_count);
        }

        [TestMethod]
        public void Cart_Service_Add_To_Cart_Works_Correct()
        {
            const int EXPECTED_PRODUCT_ID = 5;

            var cart = new Cart { Items = new List<CartItem>() };

            var product_data_mock = new Mock<IProductData>();

            var cart_store_mock = new Mock<ICartStore>();

            cart_store_mock.Setup(c => c.Cart).Returns(cart);

            var cart_service = new CartService(product_data_mock.Object, cart_store_mock.Object);

            cart_service.AddToCart(EXPECTED_PRODUCT_ID);

            Assert.Equal(1, cart.ItemsCount);
            Assert.Equal(1, cart.Items.Count);
            Assert.Equal(EXPECTED_PRODUCT_ID, cart.Items.First().ProductId);

        }

        [TestMethod]
        public void CartService_AddToCart_Increment_Quantity()
        {
            const int EXPECTED_PRODUCT_ID = 5;
            const int EXPECTED_ITEMS_COUNT = 3;

            var cart = new Cart
            {
                Items = new List<CartItem> { new CartItem { ProductId = EXPECTED_PRODUCT_ID, Quantity = EXPECTED_ITEMS_COUNT - 1 } }

            };

            var product_data_mock = new Mock<IProductData>();

            var cart_store_mock = new Mock<ICartStore>();

            cart_store_mock.Setup(c => c.Cart).Returns(cart);

            var cart_service = new CartService(product_data_mock.Object, cart_store_mock.Object);

            cart_service.AddToCart(EXPECTED_PRODUCT_ID);

            Assert.Equal(1, cart.Items.Count);
            Assert.Equal(EXPECTED_PRODUCT_ID, cart.Items.First().ProductId);
        }

        [TestMethod]
        public void CartService_RemoveFromCart_Removes_Correct_Item()
        {
            var cart = new Cart
            {
                Items = new List<CartItem>
                {
                    new CartItem {ProductId = 1, Quantity = 3},
                    new CartItem { ProductId = 2,Quantity = 1}
                }
            };


            var product_data_mock = new Mock<IProductData>();

            var cart_store_mock = new Mock<ICartStore>();

            cart_store_mock.Setup(c => c.Cart).Returns(cart);

            var cart_service = new CartService(product_data_mock.Object, cart_store_mock.Object);

            cart_service.RemoveFromCart(1);

            Assert.Equal(1, cart.Items.Count);
            Assert.Equal(2, cart.Items.First().ProductId);
        }

        [TestMethod]
        public void CartService_RemoveAll_Clear_Cart()
        {
            var cart = new Cart
            {
                Items = new List<CartItem>
                {
                    new CartItem {ProductId = 1, Quantity = 3},
                    new CartItem { ProductId = 2,Quantity = 1}
                }
            };
            var product_data_mock = new Mock<IProductData>();

            var cart_store_mock = new Mock<ICartStore>();

            cart_store_mock.Setup(c => c.Cart).Returns(cart);

            var cart_service = new CartService(product_data_mock.Object, cart_store_mock.Object);

            cart_service.RemoveAll();

            Assert.Equal(0, cart.Items.Count);
        }

        [TestMethod]
        public void CartService_Decrement_Correct()
        {
            CartItem test_item = new CartItem { ProductId = 1, Quantity = 3 };
            var cart = new Cart
            {
                Items = new List<CartItem>
                {
                    test_item,
                    new CartItem { ProductId = 2,Quantity = 1}
                }
            };
            var product_data_mock = new Mock<IProductData>();

            var cart_store_mock = new Mock<ICartStore>();

            cart_store_mock.Setup(c => c.Cart).Returns(cart);

            var cart_service = new CartService(product_data_mock.Object, cart_store_mock.Object);

            cart_service.DecrementFromCart(test_item.ProductId);

            Assert.Equal(2, cart.Items.Count);
            Assert.Equal(3, cart.ItemsCount);
            Assert.Equal(2, test_item.Quantity);

        }

        [TestMethod]
        public void CartService_Remove_Item_When_Decrement()
        {
            CartItem test_item = new CartItem { ProductId = 2, Quantity = 1 };
            var cart = new Cart
            {
                Items = new List<CartItem>
                {
                    new CartItem { ProductId = 1, Quantity = 3 },
                    test_item
                }
            };
            var product_data_mock = new Mock<IProductData>();

            var cart_store_mock = new Mock<ICartStore>();

            cart_store_mock.Setup(c => c.Cart).Returns(cart);

            var cart_service = new CartService(product_data_mock.Object, cart_store_mock.Object);

            cart_service.DecrementFromCart(test_item.ProductId);

            Assert.Equal(3, cart.ItemsCount);
            Assert.Equal(1, cart.Items.Count);
            Assert.DoesNotContain(test_item, cart.Items);
        }

        [TestMethod]
        public void TransformCart_Works_Correctly()
        {
            var cart = new Cart
            {
                Items = new List<CartItem> { new CartItem { ProductId = 1, Quantity = 4 } }
            };

            var products = new List<ProductDTO>
            {
                new ProductDTO
                {
                    Id = 1,
                    ImageUrl = "image1.jpg",
                    Name="Test",
                    Order = 0,
                    Price = 1.11m
                }
            };

            var product_data_mock = new Mock<IProductData>();

            product_data_mock
                .Setup(c => c.GetProducts(It.IsAny<ProductFilter>())).Returns(products);

            var cart_store_mock = new Mock<ICartStore>();

            cart_store_mock.Setup(c => c.Cart).Returns(cart);

            var cart_service = new CartService(product_data_mock.Object, cart_store_mock.Object);

            var result = cart_service.TransformCart();

            Assert.Equal(4, result.ItemsCount);
            Assert.Equal(1.11m, result.Items.First().Key.Price);
        }
    }

}
