using System;
using CashControl.Models;
using CashControlBack.Models;
using Microsoft.EntityFrameworkCore;

namespace CashControl.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies{ get; set; }
        public DbSet<CompanyProduct> CompanyProducts { get; set; }
        public DbSet<SalesRecord> SalesRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().ToTable("users");
            builder.Entity<Company>().ToTable("Company");
            builder.Entity<CompanyProduct>(entity =>
        {
            entity.HasKey(p => p.ProductId);
        });
        }
    }
}

