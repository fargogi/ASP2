using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyWebStore.DAL;
using MyWebStore.DomainEntities.Entities;
using MyWebStore.DomainNew.Entities;
using WebStore.Interfaces;
using MyWebStore.DomainNew.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebStore.DomainNew.DTO.Order;
using WebStore.Services.Map;

namespace WebStore.Services
{
    public class SqlOrderService : IOrderService
    {
        private readonly MyWebStoreContext _db;
        private readonly UserManager<User> _UserManager;

        public SqlOrderService(MyWebStoreContext db, UserManager<User> UserManager)
        {
            _db = db;
            _UserManager = UserManager;
        }

        public IEnumerable<OrderDTO> GetUserOrders(string UserName)
        {
            return _db.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .Where(o => o.User.UserName == UserName)
                .ToArray().Select(OrderDTO2Order.Map);
        }

        public OrderDTO GetOrderById(int id)
        {
            return _db.Orders.Include(o => o.OrderItems).FirstOrDefault(o => o.Id == id).Map();
        }

        public OrderDTO CreateOrder(CreateOrderModel OrderModel, string UserName)
        {
            var user = _UserManager.FindByNameAsync(UserName).Result;
            using (var transaction = _db.Database.BeginTransaction())
            {
                var Model = OrderModel.OrderViewModel;
                var order = new Order
                {
                    Address = Model.Address,
                    Name = Model.Name,
                    User = user,
                    Date = DateTime.Now,
                    Phone = Model.PhoneNumber
                };

                _db.Orders.Add(order);

                foreach (var item in OrderModel.Items)
                {
                    //var product_view_model = item.Key;
                    //var product = _db.Products.FirstOrDefault(p => p.Id == product_view_model.Id);
                    var product = _db.Products.FirstOrDefault(p => p.Id == item.Id);
                    if (product is null)
                        throw new InvalidOperationException($"Товар с id={item.Id} в базе не неайден");

                    var order_item = new OrderItem
                    {
                        Order = order,
                        Price = product.Price,
                        Count = item.Quantity,
                        Product = product
                    };

                    _db.OrderItems.Add(order_item);
                }

                _db.SaveChanges();

                transaction.Commit();

                return order.Map();
            }
        }
    }
}
