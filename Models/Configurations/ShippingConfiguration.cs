using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BenriShop.Models.Configurations
{
    public class ShippingConfiguration : IEntityTypeConfiguration<Shipping>
    {
        public void Configure(EntityTypeBuilder<Shipping> builder)
        {
            builder.HasKey(e => new { e.OrderId, e.ShippingId });

            builder.ToTable("SHIPPING");

            builder.HasIndex(e => e.OrderId)
                .HasName("HAVE_SHIPMENT_FK");

            builder.Property(e => e.OrderId)
                .HasColumnName("ORDERID")
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.ShippingId)
                .HasColumnName("SHIPPINGID")
                .HasMaxLength(60)
                .IsUnicode(false);

            builder.Property(e => e.Cost).HasColumnName("COST");

            builder.Property(e => e.Note)
                .HasColumnName("NOTE")
                .HasMaxLength(300);

            builder.Property(e => e.Status).HasColumnName("STATUS");

            builder.HasOne(d => d.Order)
                .WithMany(p => p.Shippings)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SHIPPING_HAVE_SHIP_ORDER");
        }
    }
}
