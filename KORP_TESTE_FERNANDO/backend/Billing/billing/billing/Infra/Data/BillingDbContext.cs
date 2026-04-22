using billing.core.Models;
using Microsoft.EntityFrameworkCore;

namespace billing.Infra.Data
{
    public class BillingDbContext : DbContext
    {
        public BillingDbContext(DbContextOptions<BillingDbContext> options) : base(options)
        {
        }

        public DbSet<InvoiceModel> InvoiceModel { get; set; }

        public DbSet<InvoiceItemModel> InvoiceItemModel { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<InvoiceModel>(entity =>
            {
                entity.ToTable("Invoices");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.SequentialNumber)
                    .IsRequired();

                entity.HasIndex(x => x.SequentialNumber)
                    .IsUnique();

                entity.Property(x => x.Status)
                    .IsRequired();

                entity.Property(x => x.CreatedAt)
                    .IsRequired();

                entity.HasMany(x => x.Items)
                    .WithOne(x => x.Invoice)
                    .HasForeignKey(x => x.InvoiceId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<InvoiceItemModel>(entity =>
            {
                entity.ToTable("InvoiceItems");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.ProductId)
                    .IsRequired();

                entity.Property(x => x.ProductCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(x => x.ProductDescription)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(x => x.Quantity)
                    .IsRequired();
            });

        }
    }
}