using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BenriShop.Models
{
    public partial class BenriShopContext : DbContext
    {
        public BenriShopContext()
        {
        }

        public BenriShopContext(DbContextOptions<BenriShopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Cartitem> Cartitem { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Color> Color { get; set; }
        public virtual DbSet<HaveTag> HaveTag { get; set; }
        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Orderitem> Orderitem { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Shipping> Shipping { get; set; }
        public virtual DbSet<Size> Size { get; set; }
        public virtual DbSet<Sizeofproducthadcolor> Sizeofproducthadcolor { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=BenriShop;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.ToTable("ACCOUNT");

                entity.Property(e => e.Username)
                    .HasColumnName("USERNAME")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .HasColumnName("ADDRESS")
                    .HasMaxLength(300);

                entity.Property(e => e.Fullname)
                    .HasColumnName("FULLNAME")
                    .HasMaxLength(100);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("PASSWORD")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Phonenumber)
                    .HasColumnName("PHONENUMBER")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasColumnName("ROLE")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Cartitem>(entity =>
            {
                entity.HasKey(e => new { e.Productid, e.Username });

                entity.ToTable("CARTITEM");

                entity.HasIndex(e => e.Productid)
                    .HasName("ADDED_FK");

                entity.HasIndex(e => e.Username)
                    .HasName("HAVE_ITEM_IN_CART_FK");

                entity.Property(e => e.Productid)
                    .HasColumnName("PRODUCTID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasColumnName("USERNAME")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Quantityincart).HasColumnName("QUANTITYINCART");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Cartitem)
                    .HasForeignKey(d => d.Productid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CARTITEM_ADDED_PRODUCT");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Cartitem)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CARTITEM_HAVE_ITEM_ACCOUNT");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("CATEGORY");

                entity.Property(e => e.Categoryid)
                    .HasColumnName("CATEGORYID")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.ToTable("COLOR");

                entity.Property(e => e.Colorid)
                    .HasColumnName("COLORID")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<HaveTag>(entity =>
            {
                entity.HasKey(e => new { e.Productid, e.Tagid });

                entity.ToTable("HAVE_TAG");

                entity.HasIndex(e => e.Productid)
                    .HasName("HAVE_TAG_FK");

                entity.HasIndex(e => e.Tagid)
                    .HasName("HAVE_TAG2_FK");

                entity.Property(e => e.Productid)
                    .HasColumnName("PRODUCTID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Tagid)
                    .HasColumnName("TAGID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.HaveTag)
                    .HasForeignKey(d => d.Productid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HAVE_TAG_HAVE_TAG_PRODUCT");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.HaveTag)
                    .HasForeignKey(d => d.Tagid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HAVE_TAG_HAVE_TAG2_TAG");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.HasKey(e => new { e.Productid, e.Imageid });

                entity.ToTable("IMAGE");

                entity.HasIndex(e => e.Productid)
                    .HasName("HAVE_IMAGE_FK");

                entity.Property(e => e.Productid)
                    .HasColumnName("PRODUCTID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Imageid)
                    .HasColumnName("IMAGEID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Link)
                    .IsRequired()
                    .HasColumnName("LINK")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Image)
                    .HasForeignKey(d => d.Productid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IMAGE_HAVE_IMAG_PRODUCT");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("ORDER");

                entity.HasIndex(e => e.Username)
                    .HasName("ORDER_FK");

                entity.Property(e => e.Orderid)
                    .HasColumnName("ORDERID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Payment).HasColumnName("PAYMENT");

                entity.Property(e => e.Username)
                    .HasColumnName("USERNAME")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.Username)
                    .HasConstraintName("FK_ORDER_ORDER_ACCOUNT");
            });

            modelBuilder.Entity<Orderitem>(entity =>
            {
                entity.HasKey(e => new { e.Productid, e.Orderid });

                entity.ToTable("ORDERITEM");

                entity.HasIndex(e => e.Orderid)
                    .HasName("HAVE_ITEM_FK");

                entity.HasIndex(e => e.Productid)
                    .HasName("ORDERED_FK");

                entity.Property(e => e.Productid)
                    .HasColumnName("PRODUCTID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Orderid)
                    .HasColumnName("ORDERID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Quantityinorder).HasColumnName("QUANTITYINORDER");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Orderitem)
                    .HasForeignKey(d => d.Orderid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ORDERITE_HAVE_ITEM_ORDER");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Orderitem)
                    .HasForeignKey(d => d.Productid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ORDERITE_ORDERED_PRODUCT");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("PRODUCT");

                entity.HasIndex(e => e.Categoryid)
                    .HasName("HAVE_CATEGORY_FK");

                entity.Property(e => e.Productid)
                    .HasColumnName("PRODUCTID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Categoryid)
                    .IsRequired()
                    .HasColumnName("CATEGORYID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnName("PRICE");

                entity.Property(e => e.Productdescription)
                    .IsRequired()
                    .HasColumnName("PRODUCTDESCRIPTION")
                    .HasMaxLength(2000);

                entity.Property(e => e.Productname)
                    .IsRequired()
                    .HasColumnName("PRODUCTNAME")
                    .HasMaxLength(200);

                entity.Property(e => e.Storagequantity).HasColumnName("STORAGEQUANTITY");
            });

            modelBuilder.Entity<Shipping>(entity =>
            {
                entity.HasKey(e => new { e.Orderid, e.Shippingid });

                entity.ToTable("SHIPPING");

                entity.HasIndex(e => e.Orderid)
                    .HasName("HAVE_SHIPMENT_FK");

                entity.Property(e => e.Orderid)
                    .HasColumnName("ORDERID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Shippingid)
                    .HasColumnName("SHIPPINGID")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Cost).HasColumnName("COST");

                entity.Property(e => e.Note)
                    .HasColumnName("NOTE")
                    .HasMaxLength(300);

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Shipping)
                    .HasForeignKey(d => d.Orderid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SHIPPING_HAVE_SHIP_ORDER");
            });

            modelBuilder.Entity<Size>(entity =>
            {
                entity.ToTable("SIZE");

                entity.Property(e => e.Sizeid)
                    .HasColumnName("SIZEID")
                    .HasMaxLength(3)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Sizeofproducthadcolor>(entity =>
            {
                entity.HasKey(e => new { e.Sizeid, e.Colorid, e.Productid });

                entity.ToTable("SIZEOFPRODUCTHADCOLOR");

                entity.HasIndex(e => e.Colorid)
                    .HasName("COLOR_HAVE_SIZE_FK");

                entity.HasIndex(e => e.Productid)
                    .HasName("PRODUCT_HAVE_SIZE_AND_COLOR_FK");

                entity.HasIndex(e => e.Sizeid)
                    .HasName("SIZE_HAVE_COLOR_FK");

                entity.Property(e => e.Sizeid)
                    .HasColumnName("SIZEID")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Colorid)
                    .HasColumnName("COLORID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Productid)
                    .HasColumnName("PRODUCTID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Quantityinsizeofcolor).HasColumnName("QUANTITYINSIZEOFCOLOR");

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.Sizeofproducthadcolor)
                    .HasForeignKey(d => d.Colorid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIZEOFPR_COLOR_HAV_COLOR");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Sizeofproducthadcolor)
                    .HasForeignKey(d => d.Productid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIZEOFPR_PRODUCT_H_PRODUCT");

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.Sizeofproducthadcolor)
                    .HasForeignKey(d => d.Sizeid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIZEOFPR_SIZE_HAVE_SIZE");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("TAG");

                entity.Property(e => e.Tagid)
                    .HasColumnName("TAGID")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
