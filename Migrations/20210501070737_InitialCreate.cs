using Microsoft.EntityFrameworkCore.Migrations;

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
                    USERNAME = table.Column<string>(unicode: false, maxLength: 40, nullable: false),
                    PASSWORD = table.Column<string>(unicode: false, maxLength: 40, nullable: false),
                    ROLE = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    PHONENUMBER = table.Column<string>(unicode: false, maxLength: 12, nullable: true),
                    FULLNAME = table.Column<string>(maxLength: 100, nullable: true),
                    ADDRESS = table.Column<string>(maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ACCOUNT", x => x.USERNAME);
                });

            migrationBuilder.CreateTable(
                name: "CATEGORY",
                columns: table => new
                {
                    CATEGORYID = table.Column<string>(unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORY", x => x.CATEGORYID);
                });

            migrationBuilder.CreateTable(
                name: "COLOR",
                columns: table => new
                {
                    COLORID = table.Column<string>(unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COLOR", x => x.COLORID);
                });

            migrationBuilder.CreateTable(
                name: "SIZE",
                columns: table => new
                {
                    SIZEID = table.Column<string>(unicode: false, maxLength: 100, nullable: false)
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
                    USER_NAME = table.Column<string>(unicode: false, maxLength: 40, nullable: false),
                    ORDER_ID = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    PAYMENT = table.Column<bool>(nullable: false),
                    STATUS = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORDER", x => new { x.USER_NAME, x.ORDER_ID });
                    table.ForeignKey(
                        name: "FK_ORDER_HAD_ORDER_ACCOUNT",
                        column: x => x.USER_NAME,
                        principalTable: "ACCOUNT",
                        principalColumn: "USERNAME",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCT",
                columns: table => new
                {
                    PRODUCT_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CATEGORY_ID = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    PRODUCT_NAME = table.Column<string>(maxLength: 200, nullable: false),
                    PRODUCT_DESCRIPTION = table.Column<string>(maxLength: 2000, nullable: false),
                    PRICE = table.Column<int>(nullable: false),
                    STORAGE_QUANTITY = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCT", x => x.PRODUCT_ID);
                    table.ForeignKey(
                        name: "FK_PRODUCT_HAVE_CATE_CATEGORY",
                        column: x => x.CATEGORY_ID,
                        principalTable: "CATEGORY",
                        principalColumn: "CATEGORYID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SHIPPING",
                columns: table => new
                {
                    SHIPPING_ID = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    SHIP_PHONE_NUMBER = table.Column<string>(unicode: false, maxLength: 12, nullable: false),
                    SHIP_FULL_NAME = table.Column<string>(maxLength: 200, nullable: false),
                    SHIP_ADRESS = table.Column<string>(maxLength: 1000, nullable: false),
                    SHIPPING_COST = table.Column<int>(nullable: false),
                    NOTE = table.Column<string>(maxLength: 2000, nullable: true),
                    USER_NAME = table.Column<string>(unicode: false, maxLength: 40, nullable: false),
                    ORDER_ID = table.Column<string>(unicode: false, maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHIPPING", x => x.SHIPPING_ID);
                    table.ForeignKey(
                        name: "FK_SHIPPING_SHIP_TO_ORDER",
                        columns: x => new { x.USER_NAME, x.ORDER_ID },
                        principalTable: "ORDER",
                        principalColumns: new[] { "USER_NAME", "ORDER_ID" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HAVETAG",
                columns: table => new
                {
                    PRODUCTID = table.Column<int>(unicode: false, maxLength: 20, nullable: false),
                    TAGID = table.Column<string>(unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HAVETAG", x => new { x.PRODUCTID, x.TAGID });
                    table.ForeignKey(
                        name: "FK_HAVETAG_PRODUCT_PRODUCTID",
                        column: x => x.PRODUCTID,
                        principalTable: "PRODUCT",
                        principalColumn: "PRODUCT_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HAVETAG_TAG_TAGID",
                        column: x => x.TAGID,
                        principalTable: "TAG",
                        principalColumn: "TAGID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IMAGE",
                columns: table => new
                {
                    PRODUCT_ID = table.Column<int>(nullable: false),
                    IMAGE_ID = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    LINK = table.Column<string>(unicode: false, maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IMAGE", x => new { x.PRODUCT_ID, x.IMAGE_ID });
                    table.ForeignKey(
                        name: "FK_IMAGE_HAVE_IMAG_PRODUCT",
                        column: x => x.PRODUCT_ID,
                        principalTable: "PRODUCT",
                        principalColumn: "PRODUCT_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SIZE_OF_PRODUCT_HAD_COLOR",
                columns: table => new
                {
                    SIZE_ID = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    COLOR_ID = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    PRODUCT_ID = table.Column<int>(nullable: false),
                    QUANTITY_IN_SIZE_OF_COLOR = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIZE_OF_PRODUCT_HAD_COLOR", x => new { x.SIZE_ID, x.COLOR_ID, x.PRODUCT_ID });
                    table.ForeignKey(
                        name: "FK_SIZE_OF__COLOR_HAV_COLOR",
                        column: x => x.COLOR_ID,
                        principalTable: "COLOR",
                        principalColumn: "COLORID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SIZE_OF__PRODUCT_H_PRODUCT",
                        column: x => x.PRODUCT_ID,
                        principalTable: "PRODUCT",
                        principalColumn: "PRODUCT_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SIZE_OF__SIZE_HAVE_SIZE",
                        column: x => x.SIZE_ID,
                        principalTable: "SIZE",
                        principalColumn: "SIZEID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CART_ITEM",
                columns: table => new
                {
                    USER_NAME = table.Column<string>(unicode: false, maxLength: 40, nullable: false),
                    CART_ITEM_ID = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    QUANTITY_IN_CART = table.Column<int>(nullable: false),
                    SIZE_ID = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    COLOR_ID = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    PRODUCT_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CART_ITEM", x => new { x.USER_NAME, x.CART_ITEM_ID });
                    table.ForeignKey(
                        name: "FK_CART_ITE_HAVE_ITEM_ACCOUNT",
                        column: x => x.USER_NAME,
                        principalTable: "ACCOUNT",
                        principalColumn: "USERNAME",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CART_ITE_HAD_PRODU_SIZE_OF_",
                        columns: x => new { x.SIZE_ID, x.COLOR_ID, x.PRODUCT_ID },
                        principalTable: "SIZE_OF_PRODUCT_HAD_COLOR",
                        principalColumns: new[] { "SIZE_ID", "COLOR_ID", "PRODUCT_ID" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ORDER_ITEM",
                columns: table => new
                {
                    USER_NAME = table.Column<string>(unicode: false, maxLength: 40, nullable: false),
                    ORDER_ID = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    ORDER_ITEM_ID = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    QUANTITY_IN_ORDER = table.Column<int>(nullable: false),
                    SIZE_ID = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    COLOR_ID = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    PRODUCT_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORDER_ITEM", x => new { x.USER_NAME, x.ORDER_ID, x.ORDER_ITEM_ID });
                    table.ForeignKey(
                        name: "FK_ORDER_IT_HAVE_ITEM_ORDER",
                        columns: x => new { x.USER_NAME, x.ORDER_ID },
                        principalTable: "ORDER",
                        principalColumns: new[] { "USER_NAME", "ORDER_ID" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ORDER_IT_HAVE_PROD_SIZE_OF_",
                        columns: x => new { x.SIZE_ID, x.COLOR_ID, x.PRODUCT_ID },
                        principalTable: "SIZE_OF_PRODUCT_HAD_COLOR",
                        principalColumns: new[] { "SIZE_ID", "COLOR_ID", "PRODUCT_ID" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "HAVE_ITEM_IN_CART_FK",
                table: "CART_ITEM",
                column: "USER_NAME");

            migrationBuilder.CreateIndex(
                name: "HAD_PRODUCT_FK",
                table: "CART_ITEM",
                columns: new[] { "SIZE_ID", "COLOR_ID", "PRODUCT_ID" });

            migrationBuilder.CreateIndex(
                name: "HAVETAG_FK",
                table: "HAVETAG",
                column: "PRODUCTID");

            migrationBuilder.CreateIndex(
                name: "HAVETAG2_FK",
                table: "HAVETAG",
                column: "TAGID");

            migrationBuilder.CreateIndex(
                name: "HAVE_IMAGE_FK",
                table: "IMAGE",
                column: "PRODUCT_ID");

            migrationBuilder.CreateIndex(
                name: "HAD_ORDER_FK",
                table: "ORDER",
                column: "USER_NAME");

            migrationBuilder.CreateIndex(
                name: "HAVE_ITEM_FK",
                table: "ORDER_ITEM",
                columns: new[] { "USER_NAME", "ORDER_ID" });

            migrationBuilder.CreateIndex(
                name: "HAVE_PRODUCT_FK",
                table: "ORDER_ITEM",
                columns: new[] { "SIZE_ID", "COLOR_ID", "PRODUCT_ID" });

            migrationBuilder.CreateIndex(
                name: "HAVE_CATEGORY_FK",
                table: "PRODUCT",
                column: "CATEGORY_ID");

            migrationBuilder.CreateIndex(
                name: "SHIP_TO_FK",
                table: "SHIPPING",
                columns: new[] { "USER_NAME", "ORDER_ID" });

            migrationBuilder.CreateIndex(
                name: "COLOR_HAVE_SIZE_FK",
                table: "SIZE_OF_PRODUCT_HAD_COLOR",
                column: "COLOR_ID");

            migrationBuilder.CreateIndex(
                name: "PRODUCT_HAVE_SIZE_AND_COLOR_FK",
                table: "SIZE_OF_PRODUCT_HAD_COLOR",
                column: "PRODUCT_ID");

            migrationBuilder.CreateIndex(
                name: "SIZE_HAVE_COLOR_FK",
                table: "SIZE_OF_PRODUCT_HAD_COLOR",
                column: "SIZE_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CART_ITEM");

            migrationBuilder.DropTable(
                name: "HAVETAG");

            migrationBuilder.DropTable(
                name: "IMAGE");

            migrationBuilder.DropTable(
                name: "ORDER_ITEM");

            migrationBuilder.DropTable(
                name: "SHIPPING");

            migrationBuilder.DropTable(
                name: "TAG");

            migrationBuilder.DropTable(
                name: "SIZE_OF_PRODUCT_HAD_COLOR");

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
