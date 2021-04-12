﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace BenriShop.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ACCOUNT",
                columns: table => new
                {
                    USERNAME = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    PASSWORD = table.Column<string>(unicode: false, maxLength: 40, nullable: false),
                    PHONENUMBER = table.Column<string>(unicode: false, maxLength: 12, nullable: true),
                    ADDRESS = table.Column<string>(maxLength: 300, nullable: true),
                    FULLNAME = table.Column<string>(maxLength: 100, nullable: true),
                    ROLE = table.Column<string>(unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ACCOUNT", x => x.USERNAME);
                });

            migrationBuilder.CreateTable(
                name: "CATEGORY",
                columns: table => new
                {
                    CATEGORYID = table.Column<string>(unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORY", x => x.CATEGORYID);
                });

            migrationBuilder.CreateTable(
                name: "COLOR",
                columns: table => new
                {
                    COLORID = table.Column<string>(unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COLOR", x => x.COLORID);
                });

            migrationBuilder.CreateTable(
                name: "SIZE",
                columns: table => new
                {
                    SIZEID = table.Column<string>(unicode: false, maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIZE", x => x.SIZEID);
                });

            migrationBuilder.CreateTable(
                name: "TAG",
                columns: table => new
                {
                    TAGID = table.Column<string>(unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAG", x => x.TAGID);
                });

            migrationBuilder.CreateTable(
                name: "ORDER",
                columns: table => new
                {
                    ORDERID = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    USERNAME = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    PAYMENT = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORDER", x => x.ORDERID);
                    table.ForeignKey(
                        name: "FK_ORDER_ORDER_ACCOUNT",
                        column: x => x.USERNAME,
                        principalTable: "ACCOUNT",
                        principalColumn: "USERNAME",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCT",
                columns: table => new
                {
                    PRODUCTID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CATEGORYID = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    PRODUCTNAME = table.Column<string>(maxLength: 200, nullable: false),
                    PRODUCTDESCRIPTION = table.Column<string>(maxLength: 2000, nullable: false),
                    PRICE = table.Column<int>(nullable: false),
                    STORAGEQUANTITY = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCT", x => x.PRODUCTID);
                    table.ForeignKey(
                        name: "FK_PRODUCT_HAVE_CATE_CATEGORY",
                        column: x => x.CATEGORYID,
                        principalTable: "CATEGORY",
                        principalColumn: "CATEGORYID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SHIPPING",
                columns: table => new
                {
                    ORDERID = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    SHIPPINGID = table.Column<string>(unicode: false, maxLength: 60, nullable: false),
                    COST = table.Column<int>(nullable: false),
                    STATUS = table.Column<int>(nullable: false),
                    NOTE = table.Column<string>(maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHIPPING", x => new { x.ORDERID, x.SHIPPINGID });
                    table.ForeignKey(
                        name: "FK_SHIPPING_HAVE_SHIP_ORDER",
                        column: x => x.ORDERID,
                        principalTable: "ORDER",
                        principalColumn: "ORDERID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CARTITEM",
                columns: table => new
                {
                    PRODUCTID = table.Column<int>(unicode: false, maxLength: 20, nullable: false),
                    USERNAME = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    QUANTITYINCART = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CARTITEM", x => new { x.PRODUCTID, x.USERNAME });
                    table.ForeignKey(
                        name: "FK_CARTITEM_ADDED_PRODUCT",
                        column: x => x.PRODUCTID,
                        principalTable: "PRODUCT",
                        principalColumn: "PRODUCTID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CARTITEM_HAVE_ITEM_ACCOUNT",
                        column: x => x.USERNAME,
                        principalTable: "ACCOUNT",
                        principalColumn: "USERNAME",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HAVE_TAG",
                columns: table => new
                {
                    PRODUCTID = table.Column<int>(unicode: false, maxLength: 20, nullable: false),
                    TAGID = table.Column<string>(unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HAVE_TAG", x => new { x.PRODUCTID, x.TAGID });
                    table.ForeignKey(
                        name: "FK_HAVE_TAG_HAVE_TAG_PRODUCT",
                        column: x => x.PRODUCTID,
                        principalTable: "PRODUCT",
                        principalColumn: "PRODUCTID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HAVE_TAG_HAVE_TAG2_TAG",
                        column: x => x.TAGID,
                        principalTable: "TAG",
                        principalColumn: "TAGID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IMAGE",
                columns: table => new
                {
                    PRODUCTID = table.Column<int>(unicode: false, maxLength: 20, nullable: false),
                    IMAGEID = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    LINK = table.Column<string>(unicode: false, maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IMAGE", x => new { x.PRODUCTID, x.IMAGEID });
                    table.ForeignKey(
                        name: "FK_IMAGE_HAVE_IMAG_PRODUCT",
                        column: x => x.PRODUCTID,
                        principalTable: "PRODUCT",
                        principalColumn: "PRODUCTID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ORDERITEM",
                columns: table => new
                {
                    PRODUCTID = table.Column<int>(unicode: false, maxLength: 20, nullable: false),
                    ORDERID = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    QUANTITYINORDER = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORDERITEM", x => new { x.PRODUCTID, x.ORDERID });
                    table.ForeignKey(
                        name: "FK_ORDERITE_HAVE_ITEM_ORDER",
                        column: x => x.ORDERID,
                        principalTable: "ORDER",
                        principalColumn: "ORDERID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ORDERITE_ORDERED_PRODUCT",
                        column: x => x.PRODUCTID,
                        principalTable: "PRODUCT",
                        principalColumn: "PRODUCTID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SIZEOFPRODUCTHADCOLOR",
                columns: table => new
                {
                    SIZEID = table.Column<string>(unicode: false, maxLength: 3, nullable: false),
                    COLORID = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    PRODUCTID = table.Column<int>(unicode: false, maxLength: 20, nullable: false),
                    QUANTITYINSIZEOFCOLOR = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIZEOFPRODUCTHADCOLOR", x => new { x.SIZEID, x.COLORID, x.PRODUCTID });
                    table.ForeignKey(
                        name: "FK_SIZEOFPR_COLOR_HAV_COLOR",
                        column: x => x.COLORID,
                        principalTable: "COLOR",
                        principalColumn: "COLORID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SIZEOFPR_PRODUCT_H_PRODUCT",
                        column: x => x.PRODUCTID,
                        principalTable: "PRODUCT",
                        principalColumn: "PRODUCTID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SIZEOFPR_SIZE_HAVE_SIZE",
                        column: x => x.SIZEID,
                        principalTable: "SIZE",
                        principalColumn: "SIZEID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ADDED_FK",
                table: "CARTITEM",
                column: "PRODUCTID");

            migrationBuilder.CreateIndex(
                name: "HAVE_ITEM_IN_CART_FK",
                table: "CARTITEM",
                column: "USERNAME");

            migrationBuilder.CreateIndex(
                name: "HAVE_TAG_FK",
                table: "HAVE_TAG",
                column: "PRODUCTID");

            migrationBuilder.CreateIndex(
                name: "HAVE_TAG2_FK",
                table: "HAVE_TAG",
                column: "TAGID");

            migrationBuilder.CreateIndex(
                name: "HAVE_IMAGE_FK",
                table: "IMAGE",
                column: "PRODUCTID");

            migrationBuilder.CreateIndex(
                name: "ORDER_FK",
                table: "ORDER",
                column: "USERNAME");

            migrationBuilder.CreateIndex(
                name: "HAVE_ITEM_FK",
                table: "ORDERITEM",
                column: "ORDERID");

            migrationBuilder.CreateIndex(
                name: "ORDERED_FK",
                table: "ORDERITEM",
                column: "PRODUCTID");

            migrationBuilder.CreateIndex(
                name: "HAVE_CATEGORY_FK",
                table: "PRODUCT",
                column: "CATEGORYID");

            migrationBuilder.CreateIndex(
                name: "HAVE_SHIPMENT_FK",
                table: "SHIPPING",
                column: "ORDERID");

            migrationBuilder.CreateIndex(
                name: "COLOR_HAVE_SIZE_FK",
                table: "SIZEOFPRODUCTHADCOLOR",
                column: "COLORID");

            migrationBuilder.CreateIndex(
                name: "PRODUCT_HAVE_SIZE_AND_COLOR_FK",
                table: "SIZEOFPRODUCTHADCOLOR",
                column: "PRODUCTID");

            migrationBuilder.CreateIndex(
                name: "SIZE_HAVE_COLOR_FK",
                table: "SIZEOFPRODUCTHADCOLOR",
                column: "SIZEID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CARTITEM");

            migrationBuilder.DropTable(
                name: "HAVE_TAG");

            migrationBuilder.DropTable(
                name: "IMAGE");

            migrationBuilder.DropTable(
                name: "ORDERITEM");

            migrationBuilder.DropTable(
                name: "SHIPPING");

            migrationBuilder.DropTable(
                name: "SIZEOFPRODUCTHADCOLOR");

            migrationBuilder.DropTable(
                name: "TAG");

            migrationBuilder.DropTable(
                name: "ORDER");

            migrationBuilder.DropTable(
                name: "COLOR");

            migrationBuilder.DropTable(
                name: "PRODUCT");

            migrationBuilder.DropTable(
                name: "SIZE");

            migrationBuilder.DropTable(
                name: "ACCOUNT");

            migrationBuilder.DropTable(
                name: "CATEGORY");
        }
    }
}
