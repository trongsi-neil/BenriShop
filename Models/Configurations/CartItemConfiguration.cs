using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenriShop.Models.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(e => new { e.ProductId, e.UserName });

            builder.ToTable("CARTITEM");

            builder.HasIndex(e => e.ProductId)
                .HasName("ADDED_FK");

            builder.HasOne(x => x.Product).WithMany(x => x.CartItems).HasForeignKey(x => x.ProductId);
            
            builder.Property(e => e.ProductId)
                .HasColumnName("PRODUCTID")
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.HasOne(x => x.Account).WithMany(x => x.CartItems).HasForeignKey(x => x.UserName);

            builder.Property(e => e.UserName)
                .HasColumnName("USERNAME")
                .HasMaxLength(20)
                .IsUnicode(false);


            builder.Property(e => e.QuantityInCart).HasColumnName("QUANTITYINCART");

            //builder.HasOne(d => d.Product)
            //    .WithMany(p => p.CartItem)
            //    .HasForeignKey(d => d.Productid)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_CARTITEM_ADDED_PRODUCT");

            //builder.HasOne(d => d.UsernameNavigation)
            //    .WithMany(p => p.Cartitem)
            //    .HasForeignKey(d => d.Username)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_CARTITEM_HAVE_ITEM_ACCOUNT");

        }
    }
}
