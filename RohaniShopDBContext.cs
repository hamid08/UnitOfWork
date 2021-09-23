using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RohaniShop.Data.Mapping;
using RohaniShop.Entities;
using RohaniShop.Entities.identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RohaniShop.Data
{
    public class RohaniShopDBContext : IdentityDbContext<User,Role,int,UserClaim,UserRole,IdentityUserLogin<int>,RoleClaim,IdentityUserToken<int>>
    {
        public RohaniShopDBContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.AddCustomRohaniShopMappings();
            builder.AddCustomIdentityMappings();
            builder.Entity<User>().Property(b => b.RegisterDateTime).HasDefaultValueSql("CONVERT(DATETIME, CONVERT(VARCHAR(20),GetDate(), 120))");
            builder.Entity<Customer>().Property(b => b.RegisterDateTime).HasDefaultValueSql("CONVERT(DATETIME, CONVERT(VARCHAR(20),GetDate(), 120))");
            builder.Entity<Driver>().Property(b => b.RegisterDateTime).HasDefaultValueSql("CONVERT(DATETIME, CONVERT(VARCHAR(20),GetDate(), 120))");
            builder.Entity<User>().Property(b => b.IsActive).HasDefaultValueSql("1");
            builder.Entity<Customer>().Property(b => b.IsActive).HasDefaultValueSql("1");
            builder.Entity<Driver>().Property(b => b.IsActive).HasDefaultValueSql("1");
           
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<TypePayment> TypePayments { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Factor> Factors { get; set; }
        public DbSet<FactorProduct> FactorProducts { get; set; }
        public DbSet<FactorDriver> FactorDrivers { get; set; }
        public DbSet<UnitOfMeasurement> UnitOfMeasurements { get; set; }

    }
}
