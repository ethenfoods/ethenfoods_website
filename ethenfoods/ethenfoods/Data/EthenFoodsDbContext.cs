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
        }

        public DbSet<Product> Products { get; set; }

    }
}
