using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyWebStore.DomainNew.Entities;

namespace MyWebStore.DAL
{
    public class MyWebStoreContext : IdentityDbContext<User>
    {
        public MyWebStoreContext(DbContextOptions<MyWebStoreContext> options) : base(options) { }

        public DbSet<Section> Sections { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
