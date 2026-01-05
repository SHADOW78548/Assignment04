using FunctionApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
namespace FunctionApp.Data
{       
    public class FunctionAppDbContext : DbContext
    {
     public FunctionAppDbContext(DbContextOptions<FunctionAppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}