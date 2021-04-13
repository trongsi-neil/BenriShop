using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenriShop.Models.Configurations
{
    public class HaveTagConfiguration : IEntityTypeConfiguration<HaveTag>
    {
        public void Configure(EntityTypeBuilder<HaveTag> builder)
        {
            builder.HasKey(e => new { e.ProductId, e.TagId });

            builder.ToTable("HAVE_TAG");

            builder.HasIndex(e => e.ProductId)
                .HasName("HAVE_TAG_FK");

            builder.HasIndex(e => e.TagId)
                .HasName("HAVE_TAG2_FK");

            builder.Property(e => e.ProductId)
                .HasColumnName("PRODUCTID")
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.TagId)
                .HasColumnName("TAGID")
                .HasMaxLength(20)
                .IsUnicode(false);

            //builder.HasOne(d => d.ProductId)
            //    .WithMany(p => p.HaveTag)
            //    .HasForeignKey(d => d.Productid)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_HAVE_TAG_HAVE_TAG_PRODUCT");

            //builder.HasOne(d => d.Tag)
            //    .WithMany(p => p.HaveTag)
            //    .HasForeignKey(d => d.Tagid)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_HAVE_TAG_HAVE_TAG2_TAG");
        }
    }
}
