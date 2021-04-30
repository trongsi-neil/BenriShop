using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenriShop.Models.Configurations
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(e => new { e.ProductId, e.ImageId });

            builder.ToTable("IMAGE");

            builder.HasIndex(e => e.ProductId)
                .HasName("HAVE_IMAGE_FK");

            builder.Property(e => e.ProductId).HasColumnName("PRODUCT_ID");

            builder.Property(e => e.ImageId)
                .HasColumnName("IMAGE_ID")
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.Link)
                .IsRequired()
                .HasColumnName("LINK")
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.HasOne(d => d.Product)
                .WithMany(p => p.Image)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IMAGE_HAVE_IMAG_PRODUCT");
        }
    }
}
