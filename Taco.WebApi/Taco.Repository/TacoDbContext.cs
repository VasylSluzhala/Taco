using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taco.Core.Entities;

namespace Taco.DAL
{
    public class TacoDbContext : DbContext
    {
        #region DbSets
        public virtual DbSet<Restaurant> Restaurants { get; set; }

        public virtual DbSet<MenuItem> MenuItems { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Core.Entities.Order> Orders { get; set; }

        public virtual DbSet<OrderItem> OrderItems { get; set; }

        internal Task<Task<List<Restaurant>>> ToListAsync()
        {
            throw new NotImplementedException();
        }
        #endregion

        public TacoDbContext(DbContextOptions<TacoDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Restaurant
            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.HasMany(x => x.Categories)
                    .WithOne(x => x.Restaurant)
                    .HasForeignKey(x => x.RestaurantId);
            });

            // Category
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(x => x.Name);

                entity.HasMany(x => x.MenuItems)
                    .WithOne(x => x.Category)
                    .HasForeignKey(x => x.CategoryName);
            });

            // Menu Item
            modelBuilder.Entity<MenuItem>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.HasMany(x => x.OrderItems)
                    .WithOne(x => x.MenuItem)
                    .HasForeignKey(x => x.MenuItemId);
            });

            // Order
            modelBuilder.Entity<Core.Entities.Order>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.HasMany(x => x.OrderItems)
                   .WithOne(x => x.Order)
                   .HasForeignKey(x => x.OrderId);
            });

            // Order Item
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.MenuItemId })
                    .HasName("PRIMARY");
            });
        }
    }
}
