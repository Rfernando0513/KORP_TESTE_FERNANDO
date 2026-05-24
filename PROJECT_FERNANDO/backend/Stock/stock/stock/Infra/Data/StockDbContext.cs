using Microsoft.EntityFrameworkCore;
using stock.core.model;

namespace stock.Infra.Data
{
    public class StockDbContext : DbContext
    {
        public StockDbContext(DbContextOptions<StockDbContext> options) : base(options)
        {
        }

        public DbSet<ProductModel> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductModel>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(x => x.Description)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(x => x.Stock)
                    .IsRequired();
            });
        }
    }
}
