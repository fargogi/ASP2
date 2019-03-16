using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyWebStore.Controllers;
using MyWebStore.DomainNew.DTO.Order;
using MyWebStore.DomainNew.ViewModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using WebStore.Interfaces;

using Assert = Xunit.Assert;

namespace WebStore.Tests
{
    [TestClass]
   public class CartControllerTests
    {
        [TestMethod]
        public void CheckOut_ModelState_Invalid_Returns_ViewModel()
        {
            var cart_service_mock = new Mock<ICartService>();
            var order_service_mock = new Mock<IOrderService>();
            var logger_mock = new Mock<ILogger<CartController>>();

            var controller = new CartController(cart_service_mock.Object, order_service_mock.Object);

            controller.ModelState.AddModelError("error", "Invalid Model");

            const string EXPECTED_NAME = "Model Name";
            var result = controller.CheckOut(new OrderViewModel { Name = EXPECTED_NAME }, logger_mock.Object);

            var view_result = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<DetailsViewModel>(view_result.ViewData.Model);

            Assert.Equal(EXPECTED_NAME, model.OrderViewModel.Name);
        }

        [TestMethod]
        public void CheckOut_Calls_Service_Returns_Redirect()
        {

            var user = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, "TestUserName") }));

            var logger_mock = new Mock<ILogger<CartController>>();
            var cart_service_mock = new Mock<ICartService>();

            cart_service_mock
                .Setup(c => c.TransformCart())
                .Returns(new CartViewModel
                {
                    Items = new Dictionary<ProductViewModel, int>
                    {
                        {new ProductViewModel(), 1 }
                    }
                });

            var orders_service_mock = new Mock<IOrderService>();

            orders_service_mock
                .Setup(o => o.CreateOrder(It.IsAny<CreateOrderModel>(), It.IsAny<string>()))
                .Returns<CreateOrderModel, string>((model, user_name) => new OrderDTO { Id = 1 });

            var controller = new CartController(cart_service_mock.Object, orders_service_mock.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext { User = user }
                }
            };

            var order_view_model = new OrderViewModel
            {
                Name = "test",
                Address = "address",
                PhoneNumber = "+7(123)456-78-90"
            };

            var result = controller.CheckOut(order_view_model, logger_mock.Object);

            var redirect_result = Assert.IsType<RedirectToActionResult>(result);

            Assert.Null(redirect_result.ControllerName);
            Assert.Equal(nameof(CartController.OrderConfirmed), redirect_result.ActionName);
            Assert.Equal(1, redirect_result.RouteValues["id"]);

        }
    }
}
