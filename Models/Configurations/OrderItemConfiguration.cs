using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenriShop.Models.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(e => new { e.ProductId, e.OrderId });

            builder.ToTable("ORDERITEM");

            builder.HasIndex(e => e.OrderId)
                .HasName("HAVE_ITEM_FK");

            builder.HasIndex(e => e.ProductId)
                .HasName("ORDERED_FK");

            builder.Property(e => e.ProductId)
                .HasColumnName("PRODUCTID")
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.OrderId)
                .HasColumnName("ORDERID")
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.QuantityInOrder).HasColumnName("QUANTITYINORDER");

            builder.HasOne(d => d.Order)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ORDERITE_HAVE_ITEM_ORDER");

            builder.HasOne(d => d.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ORDERITE_ORDERED_PRODUCT");

        }
    }
}
