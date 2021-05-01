using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenriShop.Models.Configurations
{
    public class ProductConfiguration: IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("PRODUCT");

            builder.HasIndex(e => e.CategoryId)
                .HasName("HAVE_CATEGORY_FK");

            builder.Property(e => e.ProductId)
                .HasColumnName("PRODUCT_ID")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.CategoryId)
                .IsRequired()
                .HasColumnName("CATEGORY_ID")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Price).HasColumnName("PRICE");

            builder.Property(e => e.ProductDescription)
                .IsRequired()
                .HasColumnName("PRODUCT_DESCRIPTION")
                .HasMaxLength(2000);

            builder.Property(e => e.ProductName)
                .IsRequired()
                .HasColumnName("PRODUCT_NAME")
                .HasMaxLength(200);

            builder.Property(e => e.StorageQuantity).HasColumnName("STORAGE_QUANTITY");

            builder.HasOne(d => d.Category)
                .WithMany(p => p.Product)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PRODUCT_HAVE_CATE_CATEGORY");

        }
    }
}
