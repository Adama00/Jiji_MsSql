using Jiji_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Jiji_Api.Data
{
    public class JijiDbContext : DbContext
    {
        public JijiDbContext(DbContextOptions<JijiDbContext> options) : base(options)
        {
        }

        public DbSet<Categories> Categories { get; set; }
        public DbSet<Regions> Regions { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Cart> Cart { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Products>()
                .HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Products>()
                .HasOne(p => p.Region)
                .WithMany()
                .HasForeignKey(p => p.RegionId);

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Product)
                .WithMany()
                .HasForeignKey(c => c.ProductId);
        }
    }
}
