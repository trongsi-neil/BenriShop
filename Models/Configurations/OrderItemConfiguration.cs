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
            builder.HasKey(e => new { e.UserName, e.OrderId, e.OrderItemId });

            builder.ToTable("ORDER_ITEM");

            builder.HasIndex(e => new { e.UserName, e.OrderId })
                .HasName("HAVE_ITEM_FK");

            builder.HasIndex(e => new { e.SizeId, e.ColorId, e.ProductId })
                .HasName("HAVE_PRODUCT_FK");

            builder.Property(e => e.UserName)
                .HasColumnName("USER_NAME")
                .HasMaxLength(40)
                .IsUnicode(false);

            builder.Property(e => e.OrderId)
                .HasColumnName("ORDER_ID")
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.OrderItemId)
                .HasColumnName("ORDER_ITEM_ID")
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.ColorId)
                .IsRequired()
                .HasColumnName("COLOR_ID")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.ProductId).HasColumnName("PRODUCT_ID");

            builder.Property(e => e.QuantityInOrder).HasColumnName("QUANTITY_IN_ORDER");

            builder.Property(e => e.SizeId)
                .IsRequired()
                .HasColumnName("SIZE_ID")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.HasOne(d => d.Order)
                .WithMany(p => p.OrderItem)
                .HasForeignKey(d => new { d.UserName, d.OrderId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ORDER_IT_HAVE_ITEM_ORDER");

            builder.HasOne(d => d.SizeOfProductHadColor)
                .WithMany(p => p.OrderItem)
                .HasForeignKey(d => new { d.SizeId, d.ColorId, d.ProductId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ORDER_IT_HAVE_PROD_SIZE_OF_");


        }
    }
}
