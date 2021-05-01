using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BenriShop.Models.Configurations
{
    public class ShippingConfiguration : IEntityTypeConfiguration<Shipping>
    {
        public void Configure(EntityTypeBuilder<Shipping> builder)
        {
            builder.ToTable("SHIPPING");

            builder.HasIndex(e => new { e.UserName, e.OrderId })
                .HasName("SHIP_TO_FK");

            builder.Property(e => e.ShippingId)
                .HasColumnName("SHIPPING_ID")
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.Note)
                .HasColumnName("NOTE")
                .HasMaxLength(2000);

            builder.Property(e => e.OrderId)
                .IsRequired()
                .HasColumnName("ORDER_ID")
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.ShipAdress)
                .IsRequired()
                .HasColumnName("SHIP_ADRESS")
                .HasMaxLength(1000);

            builder.Property(e => e.ShipFullName)
                .IsRequired()
                .HasColumnName("SHIP_FULL_NAME")
                .HasMaxLength(200);

            builder.Property(e => e.ShipPhoneNumber)
                .IsRequired()
                .HasColumnName("SHIP_PHONE_NUMBER")
                .HasMaxLength(12)
                .IsUnicode(false);

            builder.Property(e => e.ShippingCost).HasColumnName("SHIPPING_COST");

            builder.Property(e => e.UserName)
                .IsRequired()
                .HasColumnName("USER_NAME")
                .HasMaxLength(40)
                .IsUnicode(false);

            builder.HasOne(d => d.Order)
                .WithMany(p => p.Shipping)
                .HasForeignKey(d => new { d.UserName, d.OrderId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SHIPPING_SHIP_TO_ORDER");


        }
    }
}
