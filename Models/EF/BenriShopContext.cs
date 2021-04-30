using System;
using BenriShop.Models.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using BenriShop.Models;

namespace BenriShop.Models
{
    public partial class BenriShopContext : DbContext
    {
        public BenriShopContext()
        {
        }

        public BenriShopContext(DbContextOptions<BenriShopContext> options): base(options)
        {

        }
//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=.;Database=BenriShop;Integrated Security=True");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region modelBuilder
            //modelBuilder.Entity<Account>(entity =>
            //{
            //    entity.HasKey(e => e.UserName);

            //    entity.ToTable("ACCOUNT");

            //    entity.Property(e => e.UserName)
            //        .HasColumnName("USERNAME")
            //        .HasMaxLength(20)
            //        .IsUnicode(false);

            //    entity.Property(e => e.Address)
            //        .HasColumnName("ADDRESS")
            //        .HasMaxLength(300);

            //    entity.Property(e => e.FullName)
            //        .HasColumnName("FULLNAME")
            //        .HasMaxLength(100);

            //    entity.Property(e => e.Password)
            //        .IsRequired()
            //        .HasColumnName("PASSWORD")
            //        .HasMaxLength(40)
            //        .IsUnicode(false);

            //    entity.Property(e => e.PhoneNumber)
            //        .HasColumnName("PHONENUMBER")
            //        .HasMaxLength(12)
            //        .IsUnicode(false);

            //    entity.Property(e => e.Role)
            //        .IsRequired()
            //        .HasColumnName("ROLE")
            //        .HasMaxLength(20)
            //        .IsUnicode(false);
            //});

            //modelBuilder.Entity<CartItem>(entity =>
            //{
            //entity.HasKey(e => new { e.ProductId, e.UserName });

            //entity.ToTable("CARTITEM");

            //entity.HasIndex(e => e.ProductId)
            //    .HasName("ADDED_FK");

            ////entity.HasIndex(e => e.UserName)
            ////    .HasName("HAVE_ITEM_IN_CART_FK");
            //entity.HasOne (x => x.Product).WithMany(x => x.CartItems).HasForeignKey(x => x.ProductId);
            //    entity.Property(e => e.ProductId)
            //        .HasColumnName("PRODUCTID")
            //        .HasMaxLength(20)
            //        .IsUnicode(false);
            //    entity.HasOne(x => x.Account).WithMany(x => x.CartItems).HasForeignKey(x => x.UserName);
            //    entity.Property(e => e.UserName)
            //        .HasColumnName("USERNAME")
            //        .HasMaxLength(20)
            //        .IsUnicode(false);


            //    entity.Property(e => e.QuantityInCart).HasColumnName("QUANTITYINCART");

            //    //entity.HasOne(d => d.Product)
            //    //    .WithMany(p => p.CartItem)
            //    //    .HasForeignKey(d => d.Productid)
            //    //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    //    .HasConstraintName("FK_CARTITEM_ADDED_PRODUCT");

            //    //entity.HasOne(d => d.UsernameNavigation)
            //    //    .WithMany(p => p.Cartitem)
            //    //    .HasForeignKey(d => d.Username)
            //    //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    //    .HasConstraintName("FK_CARTITEM_HAVE_ITEM_ACCOUNT");
            //});

            //modelBuilder.Entity<Category>(entity =>
            //{
            //    entity.ToTable("CATEGORY");

            //    entity.Property(e => e.CategoryId)
            //        .HasColumnName("CATEGORYID")
            //        .HasMaxLength(10)
            //        .IsUnicode(false);
            //});

            //modelBuilder.Entity<Color>(entity =>
            //{
            //    entity.ToTable("COLOR");

            //    entity.Property(e => e.ColorId)
            //        .HasColumnName("COLORID")
            //        .HasMaxLength(10)
            //        .IsUnicode(false);
            //});

            //modelBuilder.Entity<HaveTag>(entity =>
            //{
            //    entity.HasKey(e => new { e.ProductId, e.TagId });

            //    entity.ToTable("HAVE_TAG");

            //    entity.HasIndex(e => e.ProductId)
            //        .HasName("HAVE_TAG_FK");

            //    entity.HasIndex(e => e.TagId)
            //        .HasName("HAVE_TAG2_FK");

            //    entity.Property(e => e.ProductId)
            //        .HasColumnName("PRODUCTID")
            //        .HasMaxLength(20)
            //        .IsUnicode(false);

            //    entity.Property(e => e.TagId)
            //        .HasColumnName("TAGID")
            //        .HasMaxLength(20)
            //        .IsUnicode(false);

            //    //entity.HasOne(d => d.Product)
            //    //    .WithMany(p => p.HaveTag)
            //    //    .HasForeignKey(d => d.Productid)
            //    //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    //    .HasConstraintName("FK_HAVE_TAG_HAVE_TAG_PRODUCT");

            //    //entity.HasOne(d => d.Tag)
            //    //    .WithMany(p => p.HaveTag)
            //    //    .HasForeignKey(d => d.Tagid)
            //    //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    //    .HasConstraintName("FK_HAVE_TAG_HAVE_TAG2_TAG");
            //});

            //modelBuilder.Entity<Image>(entity =>
            //{
            //    entity.HasKey(e => new { e.Productid, e.Imageid });

            //    entity.ToTable("IMAGE");

            //    entity.HasIndex(e => e.Productid)
            //        .HasName("HAVE_IMAGE_FK");

            //    entity.Property(e => e.Productid)
            //        .HasColumnName("PRODUCTID")
            //        .HasMaxLength(20)
            //        .IsUnicode(false);

            //    entity.Property(e => e.Imageid)
            //        .HasColumnName("IMAGEID")
            //        .HasMaxLength(20)
            //        .IsUnicode(false);

            //    entity.Property(e => e.Link)
            //        .IsRequired()
            //        .HasColumnName("LINK")
            //        .HasMaxLength(300)
            //        .IsUnicode(false);

            //    //entity.HasOne(d => d.Product)
            //        //.WithMany(p => p.Image)
            //        //.HasForeignKey(d => d.Productid)
            //        //.OnDelete(DeleteBehavior.ClientSetNull)
            //        //.HasConstraintName("FK_IMAGE_HAVE_IMAG_PRODUCT");
            //});

            //modelBuilder.Entity<Order>(entity =>
            //{
            //    entity.ToTable("ORDER");

            //    entity.HasIndex(e => e.UserName)
            //        .HasName("ORDER_FK");

            //    entity.Property(e => e.OrderId)
            //        .HasColumnName("ORDERID")
            //        .HasMaxLength(50)
            //        .IsUnicode(false);

            //    entity.Property(e => e.Payment).HasColumnName("PAYMENT");

            //    entity.Property(e => e.UserName)
            //        .HasColumnName("USERNAME")
            //        .HasMaxLength(20)
            //        .IsUnicode(false);

            //    entity.HasOne(d => d.Account)
            //        .WithMany(p => p.Orders)
            //        .HasForeignKey(d => d.UserName)
            //        .HasConstraintName("fk_order_order_account");
            //});

            //modelBuilder.Entity<OrderItem>(entity =>
            //{
            //    entity.HasKey(e => new { e.ProductId, e.OrderId });

            //    entity.ToTable("ORDERITEM");

            //    entity.HasIndex(e => e.OrderId)
            //        .HasName("HAVE_ITEM_FK");

            //    entity.HasIndex(e => e.ProductId)
            //        .HasName("ORDERED_FK");

            //    entity.Property(e => e.ProductId)
            //        .HasColumnName("PRODUCTID")
            //        .HasMaxLength(20)
            //        .IsUnicode(false);

            //    entity.Property(e => e.OrderId)
            //        .HasColumnName("ORDERID")
            //        .HasMaxLength(50)
            //        .IsUnicode(false);

            //    entity.Property(e => e.QuantityInOrder).HasColumnName("QUANTITYINORDER");

            //    //entity.HasOne(d => d.Order)
            //    //    .WithMany(p => p.Orderitem)
            //    //    .HasForeignKey(d => d.Orderid)
            //    //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    //    .HasConstraintName("FK_ORDERITE_HAVE_ITEM_ORDER");

            //    //entity.HasOne(d => d.Product)
            //    //    .WithMany(p => p.Orderitem)
            //    //    .HasForeignKey(d => d.Productid)
            //    //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    //    .HasConstraintName("FK_ORDERITE_ORDERED_PRODUCT");
            //});

            //modelBuilder.Entity<Product>(entity =>
            //{
            //    entity.ToTable("PRODUCT");

            //    entity.HasIndex(e => e.CategoryId)
            //        .HasName("HAVE_CATEGORY_FK");

            //    entity.Property(e => e.ProductId)
            //        .HasColumnName("PRODUCTID")
            //        .ValueGeneratedOnAdd();

            //    entity.Property(e => e.CategoryId)
            //        .IsRequired()
            //        .HasColumnName("CATEGORYID")
            //        .HasMaxLength(10)
            //        .IsUnicode(false);

            //    entity.Property(e => e.Price).HasColumnName("PRICE");

            //    entity.Property(e => e.ProductDescription)
            //        .IsRequired()
            //        .HasColumnName("PRODUCTDESCRIPTION")
            //        .HasMaxLength(2000);

            //    entity.Property(e => e.ProductName)
            //        .IsRequired()
            //        .HasColumnName("PRODUCTNAME")
            //        .HasMaxLength(200);

            //    entity.Property(e => e.Storagequantity).HasColumnName("STORAGEQUANTITY");
            //});

            //modelBuilder.Entity<Shipping>(entity =>
            //{
            //    entity.HasKey(e => new { e.OrderId, e.ShippingId });

            //    entity.ToTable("SHIPPING");

            //    entity.HasIndex(e => e.OrderId)
            //        .HasName("HAVE_SHIPMENT_FK");

            //    entity.Property(e => e.OrderId)
            //        .HasColumnName("ORDERID")
            //        .HasMaxLength(50)
            //        .IsUnicode(false);

            //    entity.Property(e => e.ShippingId)
            //        .HasColumnName("SHIPPINGID")
            //        .HasMaxLength(60)
            //        .IsUnicode(false);

            //    entity.Property(e => e.Cost).HasColumnName("COST");

            //    entity.Property(e => e.Note)
            //        .HasColumnName("NOTE")
            //        .HasMaxLength(300);

            //    entity.Property(e => e.Status).HasColumnName("STATUS");

            //    entity.HasOne(d => d.Order)
            //        .WithMany(p => p.Shippings)
            //        .HasForeignKey(d => d.OrderId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_SHIPPING_HAVE_SHIP_ORDER");
            //});

            //modelBuilder.Entity<Size>(entity =>
            //{
            //    entity.ToTable("SIZE");

            //    entity.Property(e => e.SizeId)
            //        .HasColumnName("SIZEID")
            //        .HasMaxLength(3)
            //        .IsUnicode(false);
            //});

            //modelBuilder.Entity<SizeOfProductHadColor>(entity =>
            //{
            //    entity.HasKey(e => new { e.SizeId, e.ColorId, e.ProductId });

            //    entity.ToTable("SIZEOFPRODUCTHADCOLOR");

            //    entity.HasIndex(e => e.ColorId)
            //        .HasName("COLOR_HAVE_SIZE_FK");

            //    entity.HasIndex(e => e.ProductId)
            //        .HasName("PRODUCT_HAVE_SIZE_AND_COLOR_FK");

            //    entity.HasIndex(e => e.SizeId)
            //        .HasName("SIZE_HAVE_COLOR_FK");

            //    entity.Property(e => e.SizeId)
            //        .HasColumnName("SIZEID")
            //        .HasMaxLength(3)
            //        .IsUnicode(false);

            //    entity.Property(e => e.ColorId)
            //        .HasColumnName("COLORID")
            //        .HasMaxLength(10)
            //        .IsUnicode(false);

            //    entity.Property(e => e.ProductId)
            //        .HasColumnName("PRODUCTID")
            //        .HasMaxLength(20)
            //        .IsUnicode(false);

            //    entity.Property(e => e.QuantityInSizeOfColor).HasColumnName("QUANTITYINSIZEOFCOLOR");

            //    entity.HasOne(d => d.Color)
            //        .WithMany(p => p.SizeOfProductHadColors)
            //        .HasForeignKey(d => d.ColorId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_SIZEOFPR_COLOR_HAV_COLOR");

            //    entity.HasOne(d => d.Product)
            //        .WithMany(p => p.SizeOfProductHadColors)
            //        .HasForeignKey(d => d.ProductId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_SIZEOFPR_PRODUCT_H_PRODUCT");

            //    entity.HasOne(d => d.Size)
            //        .WithMany(p => p.SizeOfProductHadColors)
            //        .HasForeignKey(d => d.SizeId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_SIZEOFPR_SIZE_HAVE_SIZE");
            //});

            //modelBuilder.Entity<Tag>(entity =>
            //{
            //    entity.ToTable("TAG");

            //    entity.Property(e => e.TagId)
            //        .HasColumnName("TAGID")
            //        .HasMaxLength(20)
            //        .IsUnicode(false);
            //});
            #endregion


            modelBuilder.ApplyConfiguration(new AccountConfiguration());

            modelBuilder.ApplyConfiguration(new CartItemConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());

            modelBuilder.ApplyConfiguration(new ColorConfiguration());
            modelBuilder.ApplyConfiguration(new HaveTagConfiguration());
            modelBuilder.ApplyConfiguration(new ImageConfiguration());

            modelBuilder.ApplyConfiguration(new OrderConfiguration());

            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ShippingConfiguration());
            modelBuilder.ApplyConfiguration(new SizeConfiguration());
            modelBuilder.ApplyConfiguration(new SizeOfProductHadColorConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);



        public DbSet<Account> Accounts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<HaveTag> HaveTags { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Shipping> Shippings { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<SizeOfProductHadColor> SizeOfProductHadColors { get; set; }
        public DbSet<Tag> Tags { get; set; }

    }
}
