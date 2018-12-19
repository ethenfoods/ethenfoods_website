using ethenfoods.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ethenfoods.Data
{
    public class EthenFoodsDbContext : IdentityDbContext<ApplicationUser>
    {
        public EthenFoodsDbContext(DbContextOptions<EthenFoodsDbContext> option) : base(option)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Product>().HasData(
                new
                {
                    ID = 1,
                    SKU = "test001",
                    Name = "Yogert Extra Syrup",
                    Price = 15.00m,
                    Quantity = 100,
                    Description = "Yogert Flavored Syrup",
                    Image = "http://ethenfood.com/wp-content/uploads/2016/05/%E5%84%AA%E9%85%AA%E4%B9%B31.jpg",
                    ProductCategory = Category.Syrup,
                    PerCase = 6,
                    PerBox = 6
                });
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }

    }
}
