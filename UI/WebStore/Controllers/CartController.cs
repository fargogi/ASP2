using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces;
using MyWebStore.DomainNew.ViewModels;
using MyWebStore.DomainNew.DTO.Order;
using Microsoft.Extensions.Logging;

namespace MyWebStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _CartService;
        private readonly IOrderService _OrderService;

        public CartController(ICartService CartService, IOrderService OrderService)
        {
            _CartService = CartService;
            _OrderService = OrderService;
        }

        public IActionResult Details()
        {
            return View(new DetailsViewModel
            {
                CartViewModel = _CartService.TransformCart(),
                OrderViewModel = new OrderViewModel()
            });
        }

        public IActionResult DecrementFromCart(int id)
        {
            _CartService.DecrementFromCart(id);
            return RedirectToAction("Details");
        }

        public IActionResult RemoveFromCart(int Id)
        {
            _CartService.RemoveFromCart(Id);
            return RedirectToAction("Details");
        }

        public IActionResult RemoveAll()
        {
            _CartService.RemoveAll();
            return RedirectToAction("Details");
        }

        public IActionResult AddToCart(int Id, string ReturnUrl)
        {
            _CartService.AddToCart(Id);
            return Redirect(ReturnUrl);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult CheckOut(OrderViewModel model, [FromServices] ILogger logger)
        {
            if (!ModelState.IsValid)
                return View("Details", new DetailsViewModel
                {
                    CartViewModel = _CartService.TransformCart(),
                    OrderViewModel = model
                });
            logger.LogInformation("Оформление заказа");
            var create_order_model = new CreateOrderModel
            {
                OrderViewModel = model,
                Items = new List<OrderItemDTO>()
            };

            var order = _OrderService.CreateOrder(
                   create_order_model,
                    User.Identity.Name);
            _CartService.RemoveAll();
            return RedirectToAction("OrderConfirmed", new { id = order.Id });
        }

        public IActionResult OrderConfirmed(int id)
        {
            ViewBag.Id = id;
            return View();
        }
    }
}