//using DataAccess.Migrations;

//using Microsoft.EntityFrameworkCore.Migrations;
using Cafe.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Infrastructure.Persistence.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        /* public DataContext()
         {
         }*/

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Ingredient> Ingredients => Set<Ingredient>();
        public DbSet<ProductIngredient> ProductIngredients => Set<ProductIngredient>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<Table> Tables => Set<Table>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<DailySalesSummary> dailySalesSummaries => Set<DailySalesSummary>();
        public DbSet<ProductionHistory> ProductionHistories => Set<ProductionHistory>();
        public DbSet<User> Users => Set<User>();
        public DbSet<OperationClaim> OperationClaims => Set<OperationClaim>();
        public DbSet<UserOperationClaim> UserOperationClaims => Set<UserOperationClaim>();
        public DbSet<AuditLog> AuditLogs =>Set<AuditLog>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Composite key
            modelBuilder.Entity<ProductIngredient>()
         .HasKey(pi => new { pi.ProductId, pi.IngredientId });

            modelBuilder.Entity<ProductIngredient>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.ProductIngredients)
                .HasForeignKey(pi => pi.ProductId);

            modelBuilder.Entity<ProductIngredient>()
                .HasOne(pi => pi.Ingredient)
                .WithMany(i => i.ProductIngredients)
                .HasForeignKey(pi => pi.IngredientId);

            // Decimal precision ayarları
            modelBuilder.Entity<OrderItem>()
                .Property(o => o.UnitPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Payment>()
                .Property(p => p.TotalAmount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Product>()
    .HasOne(p => p.Category)
    .WithMany(c => c.Products)
    .HasForeignKey(p => p.CategoryId)
    .OnDelete(DeleteBehavior.Restrict); // veya Cascade

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ProductionHistory>()
     .HasOne(ph => ph.Product)
     .WithMany(p => p.ProductionHistories)
     .HasForeignKey(ph => ph.ProductId)
     .OnDelete(DeleteBehavior.SetNull);  // 🔑 kritik ayar

            modelBuilder.Entity<UserOperationClaim>()
        .HasOne(uoc => uoc.User)
        .WithMany(u => u.UserOperationClaims) // 👈 navigation property kullanılıyor
        .HasForeignKey(uoc => uoc.UserId);

            modelBuilder.Entity<UserOperationClaim>()
                .HasOne(uoc => uoc.OperationClaim)
                .WithMany(oc => oc.UserOperationClaims) // 👈 navigation property kullanılıyor
                .HasForeignKey(uoc => uoc.OperationClaimId);

            // (Opsiyonel) Kullanıcı e-mail unique
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // (Opsiyonel) Varsayılan roller
            modelBuilder.Entity<OperationClaim>().HasData(
                new OperationClaim { Id = 1, Name = "Admin" },
                new OperationClaim { Id = 2, Name = "Garson" }
            );
            modelBuilder.Entity<DailySalesSummary>()
    .Property(d => d.TotalAmount)
    .HasPrecision(18, 2);


        }

    }
}
//Add-Migration InitialCreate -StartupProject Cafe.WebAPI -Project Cafe.Infrastructure


//Update-Database -StartupProject Cafe.WebAPI -Project Cafe.Infrastructure

//Add-Migration InitialCreate

//update-database "InitialCreate"