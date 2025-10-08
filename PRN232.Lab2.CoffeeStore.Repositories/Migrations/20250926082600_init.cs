using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PRN232.Lab2.CoffeeStore.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoryId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId");
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetailId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.OrderDetailId);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId");
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CreatedDate", "Description", "Name" },
                values: new object[,]
                {
                    { "11111111-1111-1111-1111-111111111111", new DateTime(2025, 4, 26, 15, 26, 0, 651, DateTimeKind.Local).AddTicks(1130), "Various coffee beans", "Coffee Beans" },
                    { "22222222-2222-2222-2222-222222222222", new DateTime(2025, 5, 26, 15, 26, 0, 651, DateTimeKind.Local).AddTicks(1155), "Brewing machines", "Coffee Machines" },
                    { "33333333-3333-3333-3333-333333333333", new DateTime(2025, 6, 26, 15, 26, 0, 651, DateTimeKind.Local).AddTicks(1161), "Coffee accessories", "Accessories" },
                    { "44444444-4444-4444-4444-444444444444", new DateTime(2025, 7, 26, 15, 26, 0, 651, DateTimeKind.Local).AddTicks(1173), "Assorted tea leaves", "Tea" },
                    { "55555555-5555-5555-5555-555555555555", new DateTime(2025, 8, 26, 15, 26, 0, 651, DateTimeKind.Local).AddTicks(1178), "Coffee snacks", "Snacks" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "OrderDate", "Status", "UserId" },
                values: new object[,]
                {
                    { "bbbbbbb1-bbbb-bbbb-bbbb-bbbbbbbbbbb1", new DateTime(2025, 9, 16, 15, 26, 0, 651, DateTimeKind.Local).AddTicks(1350), "Completed", "user01" },
                    { "bbbbbbb2-bbbb-bbbb-bbbb-bbbbbbbbbbb2", new DateTime(2025, 9, 17, 15, 26, 0, 651, DateTimeKind.Local).AddTicks(1367), "Pending", "user02" },
                    { "bbbbbbb3-bbbb-bbbb-bbbb-bbbbbbbbbbb3", new DateTime(2025, 9, 18, 15, 26, 0, 651, DateTimeKind.Local).AddTicks(1373), "Shipped", "user03" },
                    { "bbbbbbb4-bbbb-bbbb-bbbb-bbbbbbbbbbb4", new DateTime(2025, 9, 19, 15, 26, 0, 651, DateTimeKind.Local).AddTicks(1380), "Cancelled", "user01" },
                    { "bbbbbbb5-bbbb-bbbb-bbbb-bbbbbbbbbbb5", new DateTime(2025, 9, 20, 15, 26, 0, 651, DateTimeKind.Local).AddTicks(1386), "Completed", "user04" }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentId", "Amount", "OrderId", "PaymentDate", "PaymentMethod" },
                values: new object[,]
                {
                    { "ddddddd1-dddd-dddd-dddd-dddddddddddd1", 76.00m, "bbbbbbb1-bbbb-bbbb-bbbb-bbbbbbbbbbb1", new DateTime(2025, 9, 17, 15, 26, 0, 651, DateTimeKind.Local).AddTicks(1445), "Credit Card" },
                    { "ddddddd2-dddd-dddd-dddd-dddddddddddd2", 250.00m, "bbbbbbb2-bbbb-bbbb-bbbb-bbbbbbbbbbb2", new DateTime(2025, 9, 18, 15, 26, 0, 651, DateTimeKind.Local).AddTicks(1449), "PayPal" },
                    { "ddddddd3-dddd-dddd-dddd-dddddddddddd3", 36.00m, "bbbbbbb3-bbbb-bbbb-bbbb-bbbbbbbbbbb3", new DateTime(2025, 9, 19, 15, 26, 0, 651, DateTimeKind.Local).AddTicks(1453), "Credit Card" },
                    { "ddddddd4-dddd-dddd-dddd-dddddddddddd4", 40.00m, "bbbbbbb5-bbbb-bbbb-bbbb-bbbbbbbbbbb5", new DateTime(2025, 9, 21, 15, 26, 0, 651, DateTimeKind.Local).AddTicks(1457), "Debit Card" },
                    { "ddddddd5-dddd-dddd-dddd-dddddddddddd5", 0.00m, "bbbbbbb4-bbbb-bbbb-bbbb-bbbbbbbbbbb4", new DateTime(2025, 9, 20, 15, 26, 0, 651, DateTimeKind.Local).AddTicks(1461), "N/A" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "Description", "IsActive", "Name", "Price" },
                values: new object[,]
                {
                    { "aaaaaaa1-aaaa-aaaa-aaaa-aaaaaaaaaaa1", "11111111-1111-1111-1111-111111111111", "Smooth coffee beans", true, "Arabica Beans", 15.50m },
                    { "aaaaaaa2-aaaa-aaaa-aaaa-aaaaaaaaaaa2", "11111111-1111-1111-1111-111111111111", "Strong coffee beans", true, "Robusta Beans", 12.00m },
                    { "aaaaaaa3-aaaa-aaaa-aaaa-aaaaaaaaaaa3", "22222222-2222-2222-2222-222222222222", "Automatic espresso maker", true, "Espresso Machine", 250.00m },
                    { "aaaaaaa4-aaaa-aaaa-aaaa-aaaaaaaaaaa4", "33333333-3333-3333-3333-333333333333", "Electric grinder", true, "Coffee Grinder", 45.00m },
                    { "aaaaaaa5-aaaa-aaaa-aaaa-aaaaaaaaaaa5", "44444444-4444-4444-4444-444444444444", "Refreshing green tea", true, "Green Tea", 8.00m }
                });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "OrderDetailId", "OrderId", "ProductId", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { "ccccccc1-cccc-cccc-cccc-ccccccccccc1", "bbbbbbb1-bbbb-bbbb-bbbb-bbbbbbbbbbb1", "aaaaaaa1-aaaa-aaaa-aaaa-aaaaaaaaaaa1", 2, 15.50m },
                    { "ccccccc2-cccc-cccc-cccc-ccccccccccc2", "bbbbbbb1-bbbb-bbbb-bbbb-bbbbbbbbbbb1", "aaaaaaa4-aaaa-aaaa-aaaa-aaaaaaaaaaa4", 1, 45.00m },
                    { "ccccccc3-cccc-cccc-cccc-ccccccccccc3", "bbbbbbb2-bbbb-bbbb-bbbb-bbbbbbbbbbb2", "aaaaaaa3-aaaa-aaaa-aaaa-aaaaaaaaaaa3", 1, 250.00m },
                    { "ccccccc4-cccc-cccc-cccc-ccccccccccc4", "bbbbbbb3-bbbb-bbbb-bbbb-bbbbbbbbbbb3", "aaaaaaa2-aaaa-aaaa-aaaa-aaaaaaaaaaa2", 3, 12.00m },
                    { "ccccccc5-cccc-cccc-cccc-ccccccccccc5", "bbbbbbb5-bbbb-bbbb-bbbb-bbbbbbbbbbb5", "aaaaaaa5-aaaa-aaaa-aaaa-aaaaaaaaaaa5", 5, 8.00m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OrderId",
                table: "Payments",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
