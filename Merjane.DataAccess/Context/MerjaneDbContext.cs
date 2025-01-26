using Microsoft.EntityFrameworkCore;
using Merjane.Entities;

namespace Merjane.DataAccess.Context
{
    public class MerjaneDbContext : DbContext
    {
        public MerjaneDbContext(DbContextOptions<MerjaneDbContext> options) : base(options)
        {
        }
        public MerjaneDbContext() : base()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-Q6ISH3L\\SQLEXPRESS;Database=MarjaneDb;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True;Encrypt=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //// Configure One-to-many relationship between Order and Product
            //modelBuilder.Entity<Product>()
            //    .HasMany(o => o.Products);
            ////  .WithMany(); // Since Product doesn't have a collection for Order

            modelBuilder.Entity<Order>()
                            .HasKey(o => o.Id);

            modelBuilder.Entity<Order>()
                            .Property(e => e.Id)
                            .ValueGeneratedOnAdd();

            modelBuilder.Entity<Product>()
                            .HasKey(o => o.Id);

            modelBuilder.Entity<Product>()
                            .Property(e => e.Id)
                            .ValueGeneratedOnAdd();

            modelBuilder.Entity<Product>()
                            .HasOne(p => p.Order) // Each Product has one Order
                            .WithMany(o => o.Products) // Each Order has many Products
                            .HasForeignKey(p => p.OrderId); // Foreign key in Product
        }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
    }
}
