using Microsoft.AspNetCore.Http;
using MyWebStore.DomainEntities.Entities;
using MyWebStore.Infrastructure.Interfaces;
using MyWebStore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebStore.Infrastructure.Implementations
{
    class CookieCartService : ICartService
    {
        private readonly IProductData _productData;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _cartName;

        private Cart Cart
        {
            get
            {
                var context = _httpContextAccessor.HttpContext;
                var cookie = context.Request.Cookies[_cartName];
                Cart cart = null;

                if (cookie is null)
                {
                    cart = new Cart();
                    context.Response.Cookies.Append(_cartName, JsonConvert.SerializeObject(cart),
                    new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(2)
                    });
                }
                else
                {
                    cart = JsonConvert.DeserializeObject<Cart>(cookie);
                    context.Response.Cookies.Delete(_cartName);
                    context.Response.Cookies.Append(_cartName, cookie,
                    new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(2)
                    });
                }

                return cart;
            }
            set
            {
                var context = _httpContextAccessor.HttpContext;
                var cart_json = JsonConvert.SerializeObject(value);
                context.Response.Cookies.Delete(_cartName);
                context.Response.Cookies.Append(_cartName, cart_json,
                new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(2)
                });
            }
        }

        public CookieCartService(IProductData productData, IHttpContextAccessor httpContextAccessor)
        {
            _productData = productData;

            _httpContextAccessor = httpContextAccessor;

            var user_identity = httpContextAccessor.HttpContext.User.Identity;
            _cartName = $"{(user_identity.IsAuthenticated ? user_identity.Name : null)}";
        }

        public void AddToCart(int id)
        {
            var cart = Cart;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item is null)
                cart.Items.Add(new CartItem { ProductId = id, Quantity = 1 });
            else
                item.Quantity++;
            Cart = cart;
        }

        public void DecrementFromCart(int id)
        {
            var cart = Cart;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item is null) return;
            if (item.Quantity > 0)
                item.Quantity--;
            if (item.Quantity == 0)
                cart.Items.Remove(item);
            Cart = cart;
        }

        public void RemoveAll() => Cart = new Cart();


        public void RemoveFromCart(int id)
        {
            var cart = Cart;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item is null) return;

            cart.Items.Remove(item);
            Cart = cart;
        }

        public CartViewModel TransformCart()
        {
            var products = _productData.GetProducts(new ProductFilter
            {
                Ids = Cart.Items.Select(i => i.ProductId).ToArray()

            }).Select(p => new ProductViewModel
            {
                Id = p.Id,
                ImageUrl = p.ImageUrl,
                Name = p.Name,
                Brand = p.Brand?.Name ?? String.Empty,
                Order = p.Order,
                Price = p.Price
            }).ToArray();

            return new CartViewModel
            {
                Items = Cart.Items.ToDictionary(i => products.First(p => p.Id == i.ProductId), i => i.Quantity)
            };
        }
    }
}
