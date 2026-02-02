using Microsoft.EntityFrameworkCore;
using Zaku.Domain.Entities;

namespace Zaku.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<WarehouseInventory> WarehouseInventories { get; set; }
        public DbSet<ClientSupplierRelationship> ClientSupplierRelationships { get; set; }
        public DbSet<ProductRequest> ProductRequests { get; set; }
        public DbSet<SupplierResponse> SupplierResponses { get; set; }
        public DbSet<DeliverySchedule> DeliverySchedules { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Courier> Couriers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure entities
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Email).HasMaxLength(256).IsRequired();
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.OrderNumber).IsUnique();
                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18,2)");

                entity.HasOne(e => e.Client)
                    .WithMany()
                    .HasForeignKey(e => e.ClientId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Supplier)
                    .WithMany()
                    .HasForeignKey(e => e.SupplierId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18,2)");
                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18,2)");

                entity.HasOne(e => e.Order)
                    .WithMany()
                    .HasForeignKey(e => e.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Product)
                    .WithMany()
                    .HasForeignKey(e => e.ProductID)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Supplier)
                    .WithMany()
                    .HasForeignKey(e => e.SupplierId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Rating).HasColumnType("decimal(3,2)");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.User)
                    .WithOne(u => u.Profile)
                    .HasForeignKey<UserProfile>(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Supplier)
                    .WithMany()
                    .HasForeignKey(e => e.SupplierId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<WarehouseInventory>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Warehouse)
                    .WithMany()
                    .HasForeignKey(e => e.WarehouseId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Product)
                    .WithMany()
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ClientSupplierRelationship>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Client)
                    .WithMany()
                    .HasForeignKey(e => e.ClientId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(e => e.Supplier)
                    .WithMany()
                    .HasForeignKey(e => e.SupplierId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.Property(x => x.TotalValue)
                      .HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<ProductRequest>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Client)
                    .WithMany()
                    .HasForeignKey(e => e.ClientId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.Property(e => e.MaxPrice).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<SupplierResponse>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Request)
                    .WithMany()
                    .HasForeignKey(e => e.RequestId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Supplier)
                    .WithMany()
                    .HasForeignKey(e => e.SupplierId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Product)
                    .WithMany()
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.OfferedPrice).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<DeliverySchedule>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Supplier)
                    .WithMany()
                    .HasForeignKey(e => e.SupplierId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Client)
                    .WithMany()
                    .HasForeignKey(e => e.ClientId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Delivery>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Order)
                    .WithMany()
                    .HasForeignKey(e => e.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Courier)
                    .WithMany()
                    .HasForeignKey(e => e.CourierId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(e => e.Schedule)
                    .WithMany()
                    .HasForeignKey(e => e.ScheduleId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Courier>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Supplier)
                    .WithMany()
                    .HasForeignKey(e => e.SupplierId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Add more configurations...
        }
    }
}
