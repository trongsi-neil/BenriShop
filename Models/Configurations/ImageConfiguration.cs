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
            builder.HasKey(e => new { e.ProductId, e.Imageid });

            builder.ToTable("IMAGE");

            builder.HasIndex(e => e.ProductId)
                .HasName("HAVE_IMAGE_FK");


            builder.HasOne(x => x.Product).WithMany(x => x.Images).HasForeignKey(x => x.ProductId);
            
            builder.Property(e => e.ProductId)
                .HasColumnName("PRODUCTID")
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.Imageid)
                .HasColumnName("IMAGEID")
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.Link)
                .IsRequired()
                .HasColumnName("LINK")
                .HasMaxLength(300)
                .IsUnicode(false);

            //builder.HasOne(d => d.Product)
            //.WithMany(p => p.Image)
            //.HasForeignKey(d => d.Productid)
            //.OnDelete(DeleteBehavior.ClientSetNull)
            //.HasConstraintName("FK_IMAGE_HAVE_IMAG_PRODUCT");
        }
    }
}
