using MyWebStore.DomainNew.Entities;
using MyWebStore.DomainNew.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.Services
{
    public class CartService : ICartService
    {
        private readonly ICartStore _cartStore;
        private readonly IProductData _productData;

        public CartService(IProductData productData, ICartStore cartStore )
        {
            _cartStore = cartStore;
            _productData = productData;
        }

        public void AddToCart(int id)
        {
            var cart = _cartStore.Cart;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item is null)
                cart.Items.Add(new CartItem { ProductId = id, Quantity = 1 });
            else
                item.Quantity++;
            _cartStore.Cart = cart;
        }

        public void DecrementFromCart(int id)
        {
            var cart = _cartStore.Cart;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item is null) return;
            if (item.Quantity > 0)
                item.Quantity--;
            if (item.Quantity == 0)
                cart.Items.Remove(item);
            _cartStore.Cart = cart;
        }

        public void RemoveAll()
        {
           var cart = _cartStore.Cart;
            cart.Items.Clear();
            _cartStore.Cart = cart;

        }

        public void RemoveFromCart(int id)
        {
            var cart = _cartStore.Cart;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item is null) return;

            cart.Items.Remove(item);
            _cartStore.Cart = cart;
        }

        public CartViewModel TransformCart()
        {
            var products = _productData.GetProducts(new ProductFilter
            {
                Ids = _cartStore.Cart.Items.Select(i => i.ProductId).ToArray()

            }).Products.Select(p => new ProductViewModel
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
                Items = _cartStore.Cart.Items.ToDictionary(i => products.First(p => p.Id == i.ProductId), i => i.Quantity)
            };
        }
    }
}
