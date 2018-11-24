using ShopNetwork.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNetwork.DAL.Context
{
    public class ShopNetworkContext : DbContext
    {
        public ShopNetworkContext()
            : base("ShopNetwork")
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Market> Markets { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<SubGroup> SubGroups { get; set; }
        public DbSet<New> News { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Cart> Carts { get; set; }
    }
}
