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

            builder.HasIndex(e => e.CategoryId).HasName("HAVE_CATEGORY_FK");

            builder.Property(e => e.ProductId).HasColumnName("PRODUCTID").UseIdentityColumn();

            builder.Property(e => e.CategoryId).IsRequired().HasColumnName("CATEGORYID").HasMaxLength(10).IsUnicode(false);

            builder.Property(e => e.Price).HasColumnName("PRICE");

            builder.Property(e => e.ProductDescription).IsRequired().HasColumnName("PRODUCTDESCRIPTION").HasMaxLength(2000);

            builder.Property(e => e.ProductName).IsRequired().HasColumnName("PRODUCTNAME").HasMaxLength(200);

            builder.Property(e => e.StorageQuantity).HasColumnName("STORAGEQUANTITY");

        }
    }
}
