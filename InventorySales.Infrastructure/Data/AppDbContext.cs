using System;
using InventorySales.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace InventorySales.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options ) : base(options) {}
    public DbSet<Product> Products {get; set;}
    public DbSet<User> Users {get; set;}


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
{
    entity.HasKey(e => e.Id);
    entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
    entity.HasIndex(e => e.Username).IsUnique();  // Username unique
    entity.Property(e => e.PasswordHash).IsRequired();
    entity.Property(e => e.Role).IsRequired().HasMaxLength(20);
});

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
             entity.Property(e => e.PurchasePrice).HasColumnType("decimal(18,2)");
            entity.Property(e => e.SalePrice).HasColumnType("decimal(18,2)");
    
        });
    }
}
