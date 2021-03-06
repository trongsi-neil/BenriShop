// <auto-generated />
using BenriShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BenriShop.Migrations
{
    [DbContext(typeof(BenriShopContext))]
    [Migration("20210513183701_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BenriShop.Models.Account", b =>
                {
                    b.Property<string>("UserName")
                        .HasColumnName("USERNAME")
                        .HasColumnType("varchar(40)")
                        .HasMaxLength(40)
                        .IsUnicode(false);

                    b.Property<string>("Address")
                        .HasColumnName("ADDRESS")
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.Property<string>("FullName")
                        .HasColumnName("FULLNAME")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnName("PASSWORD")
                        .HasColumnType("varchar(40)")
                        .HasMaxLength(40)
                        .IsUnicode(false);

                    b.Property<string>("PhoneNumber")
                        .HasColumnName("PHONENUMBER")
                        .HasColumnType("varchar(12)")
                        .HasMaxLength(12)
                        .IsUnicode(false);

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnName("ROLE")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.HasKey("UserName");

                    b.ToTable("ACCOUNT");
                });

            modelBuilder.Entity("BenriShop.Models.CartItem", b =>
                {
                    b.Property<string>("UserName")
                        .HasColumnName("USER_NAME")
                        .HasColumnType("varchar(40)")
                        .HasMaxLength(40)
                        .IsUnicode(false);

                    b.Property<string>("CartItemId")
                        .HasColumnName("CART_ITEM_ID")
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200)
                        .IsUnicode(false);

                    b.Property<string>("ColorId")
                        .IsRequired()
                        .HasColumnName("COLOR_ID")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<int>("ProductId")
                        .HasColumnName("PRODUCT_ID")
                        .HasColumnType("int");

                    b.Property<int>("QuantityInCart")
                        .HasColumnName("QUANTITY_IN_CART")
                        .HasColumnType("int");

                    b.Property<string>("SizeId")
                        .IsRequired()
                        .HasColumnName("SIZE_ID")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.HasKey("UserName", "CartItemId");

                    b.HasIndex("UserName")
                        .HasName("HAVE_ITEM_IN_CART_FK");

                    b.HasIndex("SizeId", "ColorId", "ProductId")
                        .HasName("HAD_PRODUCT_FK");

                    b.ToTable("CART_ITEM");
                });

            modelBuilder.Entity("BenriShop.Models.Category", b =>
                {
                    b.Property<string>("CategoryId")
                        .HasColumnName("CATEGORYID")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.HasKey("CategoryId");

                    b.ToTable("CATEGORY");
                });

            modelBuilder.Entity("BenriShop.Models.Color", b =>
                {
                    b.Property<string>("ColorId")
                        .HasColumnName("COLORID")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.HasKey("ColorId");

                    b.ToTable("COLOR");
                });

            modelBuilder.Entity("BenriShop.Models.HaveTag", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnName("PRODUCTID")
                        .HasColumnType("int")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<string>("TagId")
                        .HasColumnName("TAGID")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.HasKey("ProductId", "TagId");

                    b.HasIndex("ProductId")
                        .HasName("HAVETAG_FK");

                    b.HasIndex("TagId")
                        .HasName("HAVETAG2_FK");

                    b.ToTable("HAVETAG");
                });

            modelBuilder.Entity("BenriShop.Models.Image", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnName("PRODUCT_ID")
                        .HasColumnType("int");

                    b.Property<string>("ImageId")
                        .HasColumnName("IMAGE_ID")
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200)
                        .IsUnicode(false);

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnName("LINK")
                        .HasColumnType("varchar(500)")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.HasKey("ProductId", "ImageId");

                    b.HasIndex("ProductId")
                        .HasName("HAVE_IMAGE_FK");

                    b.ToTable("IMAGE");
                });

            modelBuilder.Entity("BenriShop.Models.Order", b =>
                {
                    b.Property<string>("UserName")
                        .HasColumnName("USER_NAME")
                        .HasColumnType("varchar(40)")
                        .HasMaxLength(40)
                        .IsUnicode(false);

                    b.Property<string>("OrderId")
                        .HasColumnName("ORDER_ID")
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200)
                        .IsUnicode(false);

                    b.Property<bool>("Payment")
                        .HasColumnName("PAYMENT")
                        .HasColumnType("bit");

                    b.Property<int>("Status")
                        .HasColumnName("STATUS")
                        .HasColumnType("int");

                    b.HasKey("UserName", "OrderId");

                    b.HasIndex("UserName")
                        .HasName("HAD_ORDER_FK");

                    b.ToTable("ORDER");
                });

            modelBuilder.Entity("BenriShop.Models.OrderItem", b =>
                {
                    b.Property<string>("UserName")
                        .HasColumnName("USER_NAME")
                        .HasColumnType("varchar(40)")
                        .HasMaxLength(40)
                        .IsUnicode(false);

                    b.Property<string>("OrderId")
                        .HasColumnName("ORDER_ID")
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200)
                        .IsUnicode(false);

                    b.Property<string>("OrderItemId")
                        .HasColumnName("ORDER_ITEM_ID")
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200)
                        .IsUnicode(false);

                    b.Property<string>("ColorId")
                        .IsRequired()
                        .HasColumnName("COLOR_ID")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<int>("ProductId")
                        .HasColumnName("PRODUCT_ID")
                        .HasColumnType("int");

                    b.Property<int>("QuantityInOrder")
                        .HasColumnName("QUANTITY_IN_ORDER")
                        .HasColumnType("int");

                    b.Property<string>("SizeId")
                        .IsRequired()
                        .HasColumnName("SIZE_ID")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.HasKey("UserName", "OrderId", "OrderItemId");

                    b.HasIndex("UserName", "OrderId")
                        .HasName("HAVE_ITEM_FK");

                    b.HasIndex("SizeId", "ColorId", "ProductId")
                        .HasName("HAVE_PRODUCT_FK");

                    b.ToTable("ORDER_ITEM");
                });

            modelBuilder.Entity("BenriShop.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("PRODUCT_ID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryId")
                        .IsRequired()
                        .HasColumnName("CATEGORY_ID")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<bool>("IsDisable")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IS_DISABLE")
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("Price")
                        .HasColumnName("PRICE")
                        .HasColumnType("int");

                    b.Property<string>("ProductDescription")
                        .IsRequired()
                        .HasColumnName("PRODUCT_DESCRIPTION")
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnName("PRODUCT_NAME")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<int>("StorageQuantity")
                        .HasColumnName("STORAGE_QUANTITY")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId")
                        .HasName("HAVE_CATEGORY_FK");

                    b.ToTable("PRODUCT");
                });

            modelBuilder.Entity("BenriShop.Models.Shipping", b =>
                {
                    b.Property<string>("ShippingId")
                        .HasColumnName("SHIPPING_ID")
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200)
                        .IsUnicode(false);

                    b.Property<string>("Note")
                        .HasColumnName("NOTE")
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.Property<string>("OrderId")
                        .IsRequired()
                        .HasColumnName("ORDER_ID")
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200)
                        .IsUnicode(false);

                    b.Property<string>("ShipAdress")
                        .IsRequired()
                        .HasColumnName("SHIP_ADRESS")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<string>("ShipFullName")
                        .IsRequired()
                        .HasColumnName("SHIP_FULL_NAME")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("ShipPhoneNumber")
                        .IsRequired()
                        .HasColumnName("SHIP_PHONE_NUMBER")
                        .HasColumnType("varchar(12)")
                        .HasMaxLength(12)
                        .IsUnicode(false);

                    b.Property<int>("ShippingCost")
                        .HasColumnName("SHIPPING_COST")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnName("USER_NAME")
                        .HasColumnType("varchar(40)")
                        .HasMaxLength(40)
                        .IsUnicode(false);

                    b.HasKey("ShippingId");

                    b.HasIndex("UserName", "OrderId")
                        .HasName("SHIP_TO_FK");

                    b.ToTable("SHIPPING");
                });

            modelBuilder.Entity("BenriShop.Models.Size", b =>
                {
                    b.Property<string>("SizeId")
                        .HasColumnName("SIZEID")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.HasKey("SizeId");

                    b.ToTable("SIZE");
                });

            modelBuilder.Entity("BenriShop.Models.SizeOfProductHadColor", b =>
                {
                    b.Property<string>("SizeId")
                        .HasColumnName("SIZE_ID")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("ColorId")
                        .HasColumnName("COLOR_ID")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<int>("ProductId")
                        .HasColumnName("PRODUCT_ID")
                        .HasColumnType("int");

                    b.Property<int>("QuantityInSizeOfColor")
                        .HasColumnName("QUANTITY_IN_SIZE_OF_COLOR")
                        .HasColumnType("int");

                    b.HasKey("SizeId", "ColorId", "ProductId");

                    b.HasIndex("ColorId")
                        .HasName("COLOR_HAVE_SIZE_FK");

                    b.HasIndex("ProductId")
                        .HasName("PRODUCT_HAVE_SIZE_AND_COLOR_FK");

                    b.HasIndex("SizeId")
                        .HasName("SIZE_HAVE_COLOR_FK");

                    b.ToTable("SIZE_OF_PRODUCT_HAD_COLOR");
                });

            modelBuilder.Entity("BenriShop.Models.Tag", b =>
                {
                    b.Property<string>("TagId")
                        .HasColumnName("TAGID")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.HasKey("TagId");

                    b.ToTable("TAG");
                });

            modelBuilder.Entity("BenriShop.Models.CartItem", b =>
                {
                    b.HasOne("BenriShop.Models.Account", "UserNameNavigation")
                        .WithMany("CartItem")
                        .HasForeignKey("UserName")
                        .HasConstraintName("FK_CART_ITE_HAVE_ITEM_ACCOUNT")
                        .IsRequired();

                    b.HasOne("BenriShop.Models.SizeOfProductHadColor", "SizeOfProductHadColor")
                        .WithMany("CartItem")
                        .HasForeignKey("SizeId", "ColorId", "ProductId")
                        .HasConstraintName("FK_CART_ITE_HAD_PRODU_SIZE_OF_")
                        .IsRequired();
                });

            modelBuilder.Entity("BenriShop.Models.HaveTag", b =>
                {
                    b.HasOne("BenriShop.Models.Product", "Product")
                        .WithMany("HaveTag")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BenriShop.Models.Tag", "Tag")
                        .WithMany("HaveTag")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BenriShop.Models.Image", b =>
                {
                    b.HasOne("BenriShop.Models.Product", "Product")
                        .WithMany("Image")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_IMAGE_HAVE_IMAG_PRODUCT")
                        .IsRequired();
                });

            modelBuilder.Entity("BenriShop.Models.Order", b =>
                {
                    b.HasOne("BenriShop.Models.Account", "UserNameNavigation")
                        .WithMany("Order")
                        .HasForeignKey("UserName")
                        .HasConstraintName("FK_ORDER_HAD_ORDER_ACCOUNT")
                        .IsRequired();
                });

            modelBuilder.Entity("BenriShop.Models.OrderItem", b =>
                {
                    b.HasOne("BenriShop.Models.Order", "Order")
                        .WithMany("OrderItem")
                        .HasForeignKey("UserName", "OrderId")
                        .HasConstraintName("FK_ORDER_IT_HAVE_ITEM_ORDER")
                        .IsRequired();

                    b.HasOne("BenriShop.Models.SizeOfProductHadColor", "SizeOfProductHadColor")
                        .WithMany("OrderItem")
                        .HasForeignKey("SizeId", "ColorId", "ProductId")
                        .HasConstraintName("FK_ORDER_IT_HAVE_PROD_SIZE_OF_")
                        .IsRequired();
                });

            modelBuilder.Entity("BenriShop.Models.Product", b =>
                {
                    b.HasOne("BenriShop.Models.Category", "Category")
                        .WithMany("Product")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FK_PRODUCT_HAVE_CATE_CATEGORY")
                        .IsRequired();
                });

            modelBuilder.Entity("BenriShop.Models.Shipping", b =>
                {
                    b.HasOne("BenriShop.Models.Order", "Order")
                        .WithMany("Shipping")
                        .HasForeignKey("UserName", "OrderId")
                        .HasConstraintName("FK_SHIPPING_SHIP_TO_ORDER")
                        .IsRequired();
                });

            modelBuilder.Entity("BenriShop.Models.SizeOfProductHadColor", b =>
                {
                    b.HasOne("BenriShop.Models.Color", "Color")
                        .WithMany("SizeOfProductHadColor")
                        .HasForeignKey("ColorId")
                        .HasConstraintName("FK_SIZE_OF__COLOR_HAV_COLOR")
                        .IsRequired();

                    b.HasOne("BenriShop.Models.Product", "Product")
                        .WithMany("SizeOfProductHadColor")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_SIZE_OF__PRODUCT_H_PRODUCT")
                        .IsRequired();

                    b.HasOne("BenriShop.Models.Size", "Size")
                        .WithMany("SizeOfProductHadColor")
                        .HasForeignKey("SizeId")
                        .HasConstraintName("FK_SIZE_OF__SIZE_HAVE_SIZE")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
