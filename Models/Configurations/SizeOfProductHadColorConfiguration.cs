using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenriShop.Models.Configurations
{
    public class SizeOfProductHadColorConfiguration : IEntityTypeConfiguration<SizeOfProductHadColor>
    {
        public void Configure(EntityTypeBuilder<SizeOfProductHadColor> builder)
        {
            builder.HasKey(e => new {e.SizeId, e.ColorId, e.ProductId });

            builder.ToTable("SIZEOFPRODUCTHADCOLOR");

            builder.HasIndex(e => e.ColorId)
                .HasName("COLOR_HAVE_SIZE_FK");

            builder.HasIndex(e => e.ProductId)
                .HasName("PRODUCT_HAVE_SIZE_AND_COLOR_FK");

            builder.HasIndex(e => e.SizeId)
                .HasName("SIZE_HAVE_COLOR_FK");

            builder.Property(e => e.SizeId)
                .HasColumnName("SIZEID")
                .HasMaxLength(3)
                .IsUnicode(false);

            builder.Property(e => e.ColorId)
                .HasColumnName("COLORID")
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.ProductId)
                .HasColumnName("PRODUCTID")
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.QuantityInSizeOfColor).HasColumnName("QUANTITYINSIZEOFCOLOR");

            builder.HasOne(d => d.Color)
                .WithMany(p => p.SizeOfProductHadColors)
                .HasForeignKey(d => d.ColorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SIZEOFPR_COLOR_HAV_COLOR");

            builder.HasOne(d => d.Product)
                .WithMany(p => p.SizeOfProductHadColors)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SIZEOFPR_PRODUCT_H_PRODUCT");

            builder.HasOne(d => d.Size)
                .WithMany(p => p.SizeOfProductHadColors)
                .HasForeignKey(d => d.SizeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SIZEOFPR_SIZE_HAVE_SIZE");

        }
    }
}
