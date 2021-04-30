using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BenriShop.Models.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(e => new { e.UserName, e.CartItemId });

            builder.ToTable("CART_ITEM");

            builder.HasIndex(e => e.UserName)
                .HasName("HAVE_ITEM_IN_CART_FK");

            builder.HasIndex(e => new { e.SizeId, e.ColorId, e.ProductId })
                .HasName("HAD_PRODUCT_FK");

            builder.Property(e => e.UserName)
                .HasColumnName("USER_NAME")
                .HasMaxLength(40)
                .IsUnicode(false);

            builder.Property(e => e.CartItemId)
                .HasColumnName("CART_ITEM_ID")
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.ColorId)
                .IsRequired()
                .HasColumnName("COLOR_ID")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.ProductId).HasColumnName("PRODUCT_ID");

            builder.Property(e => e.QuantityInCart).HasColumnName("QUANTITY_IN_CART");

            builder.Property(e => e.SizeId)
                .IsRequired()
                .HasColumnName("SIZE_ID")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.HasOne(d => d.UserNameNavigation)
                .WithMany(p => p.CartItem)
                .HasForeignKey(d => d.UserName)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CART_ITE_HAVE_ITEM_ACCOUNT");

            builder.HasOne(d => d.SizeOfProductHadColor)
                .WithMany(p => p.CartItem)
                .HasForeignKey(d => new { d.SizeId, d.ColorId, d.ProductId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CART_ITE_HAD_PRODU_SIZE_OF_");

        }
    }
}
