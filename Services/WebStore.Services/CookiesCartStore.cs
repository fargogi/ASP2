using Microsoft.AspNetCore.Http;
using MyWebStore.DomainNew.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.Services
{
    public class CookiesCartStore : ICartStore
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _cartName;

        public Cart Cart
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

        public CookiesCartStore(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            var user_identity = httpContextAccessor.HttpContext.User.Identity;
            _cartName = $"{(user_identity.IsAuthenticated ? user_identity.Name : null)}";
        }

    }
}
