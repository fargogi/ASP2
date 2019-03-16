using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyWebStore.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebStore.Interfaces.Api;
using Assert = Xunit.Assert;

namespace WebStore.Tests
{
    [TestClass]
    public class HomeControllerTests
    {
        private HomeController _Controller;

        [TestInitialize]
        public void Initialize() => _Controller = new HomeController();

        [TestMethod]
        public void Index_Returns_View()
        {
            var result = _Controller.Index();

            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void ValueServiceTest_Method_Returns_With_Values()
        {
            var data = new string[] { "1", "2"};
            var expected_length = data.Length;

            var value_service_mock = new Mock<IValueService>();
            value_service_mock
                .Setup(s=>s.Get()).
            Returns(data);
            
            var result = _Controller.ValuesServiceTest(value_service_mock.Object);

            var view_result = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<IEnumerable<string>>(view_result.ViewData.Model);

            var actual_length = model.Count();

            Assert.Equal(expected_length, actual_length);
        }

        [TestMethod]
        public void ErrorStatus_404_Redirect_To_ErrorPage404()
        {
            var result = _Controller.ErrorStatus("404");

            var redirect_to_action_result = Assert.IsType<RedirectToActionResult>(result);

            Assert.Null(redirect_to_action_result.ControllerName);
            Assert.Equal(nameof(HomeController.ErrorPage404), redirect_to_action_result.ActionName);
        }


        [TestMethod]
        public void ErrorStatus_Another_Returns_Content_Result()
        {
            var error_id = "500";
            var expected_result = $"Код ошибки {error_id}";
            var result = _Controller.ErrorStatus(error_id);

            var content_result = Assert.IsType<ContentResult>(result);

            Assert.Equal(expected_result, content_result.Content);
        }

        [TestMethod]
        public void ContactUs_Returns_View()
        {
            var result = _Controller.ContactUs();

            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void Cart_Returns_View()
        {
            var result = _Controller.Cart();

            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void BlogSingle_Returns_View()
        {
            var result = _Controller.BlogSingle();

            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void Blog_Returns_View()
        {
            var result = _Controller.Blog();

            Assert.IsType<ViewResult>(result);
        }
    }

}
