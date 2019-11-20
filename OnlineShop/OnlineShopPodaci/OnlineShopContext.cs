using Microsoft.EntityFrameworkCore;
using OnlineShopPodaci.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShopPodaci
{
    public class OnlineShopContext:DbContext
    {
        public DbSet<User> user { get; set; }
        public DbSet<CardType> cardtype { get; set; }
        public DbSet<Cart> cart { get; set; }
        public DbSet<CreditCard> creditcard { get; set; }
        public DbSet<Category> category { get; set; }
        public DbSet<City> city { get; set; }
        public DbSet<CartDetails> cartdetails { get; set; }
        public DbSet<Gender> gender { get; set; }
        public DbSet<Manufacturer> manufacturer { get; set; }
        public DbSet<Order> order { get; set; }
        public DbSet<Product> product { get; set; }
        public DbSet<SubCategory> subcategory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartDetails>().HasKey(c => new { c.CartID, c.ProductID });

            modelBuilder.Entity<CartDetails>()
                .HasOne(cd => cd.Product)
                .WithMany(c => c._CartDetails)
                .HasForeignKey(cd => cd.ProductID);

            modelBuilder.Entity<CartDetails>()
                .HasOne(cd => cd.Cart)
                .WithMany(c => c._CartDetails)
                .HasForeignKey(cd => cd.CartID);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=app.fit.ba,1431;Database=OnlineShopDB;Trusted_Connection=False; MultipleActiveResultSets=true;User=OnlineShopUser;Password=ANA116m125");

        }
    }
}
