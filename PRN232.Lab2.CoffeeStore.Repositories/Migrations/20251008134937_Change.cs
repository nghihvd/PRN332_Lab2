using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PRN232.Lab2.CoffeeStore.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class Change : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Orders_OrderId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_OrderId",
                table: "Payments");

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: "ddddddd1-dddd-dddd-dddd-dddddddddddd1");

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: "ddddddd2-dddd-dddd-dddd-dddddddddddd2");

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: "ddddddd3-dddd-dddd-dddd-dddddddddddd3");

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: "ddddddd4-dddd-dddd-dddd-dddddddddddd4");

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: "ddddddd5-dddd-dddd-dddd-dddddddddddd5");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PaymentDate",
                table: "Payments");

            migrationBuilder.AddColumn<string>(
                name: "PaymentId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "11111111-1111-1111-1111-111111111111",
                column: "CreatedDate",
                value: new DateTime(2025, 5, 8, 20, 49, 36, 700, DateTimeKind.Local).AddTicks(1148));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "22222222-2222-2222-2222-222222222222",
                column: "CreatedDate",
                value: new DateTime(2025, 6, 8, 20, 49, 36, 700, DateTimeKind.Local).AddTicks(7723));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "33333333-3333-3333-3333-333333333333",
                column: "CreatedDate",
                value: new DateTime(2025, 7, 8, 20, 49, 36, 700, DateTimeKind.Local).AddTicks(7738));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "44444444-4444-4444-4444-444444444444",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 8, 20, 49, 36, 700, DateTimeKind.Local).AddTicks(7745));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "55555555-5555-5555-5555-555555555555",
                column: "CreatedDate",
                value: new DateTime(2025, 9, 8, 20, 49, 36, 700, DateTimeKind.Local).AddTicks(7751));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: "bbbbbbb1-bbbb-bbbb-bbbb-bbbbbbbbbbb1",
                columns: new[] { "OrderDate", "PaymentId" },
                values: new object[] { new DateTime(2025, 9, 28, 20, 49, 36, 701, DateTimeKind.Local).AddTicks(3336), "pay-cc" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: "bbbbbbb2-bbbb-bbbb-bbbb-bbbbbbbbbbb2",
                columns: new[] { "OrderDate", "PaymentId" },
                values: new object[] { new DateTime(2025, 9, 29, 20, 49, 36, 701, DateTimeKind.Local).AddTicks(3594), "pay-pp" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: "bbbbbbb3-bbbb-bbbb-bbbb-bbbbbbbbbbb3",
                columns: new[] { "OrderDate", "PaymentId" },
                values: new object[] { new DateTime(2025, 9, 30, 20, 49, 36, 701, DateTimeKind.Local).AddTicks(3603), "pay-cc" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: "bbbbbbb4-bbbb-bbbb-bbbb-bbbbbbbbbbb4",
                columns: new[] { "OrderDate", "PaymentId" },
                values: new object[] { new DateTime(2025, 10, 1, 20, 49, 36, 701, DateTimeKind.Local).AddTicks(3610), "pay-na" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: "bbbbbbb5-bbbb-bbbb-bbbb-bbbbbbbbbbb5",
                columns: new[] { "OrderDate", "PaymentId" },
                values: new object[] { new DateTime(2025, 10, 2, 20, 49, 36, 701, DateTimeKind.Local).AddTicks(3618), "pay-dc" });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentId", "PaymentMethod", "Status" },
                values: new object[,]
                {
                    { "pay-cash", "Cash", "Active" },
                    { "pay-cc", "Credit Card", "Active" },
                    { "pay-dc", "Debit Card", "Active" },
                    { "pay-na", "N/A", "Inactive" },
                    { "pay-pp", "PayPal", "Active" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentId",
                table: "Orders",
                column: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Payments_PaymentId",
                table: "Orders",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "PaymentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Payments_PaymentId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PaymentId",
                table: "Orders");

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: "pay-cash");

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: "pay-cc");

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: "pay-dc");

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: "pay-na");

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: "pay-pp");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Orders");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Payments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "OrderId",
                table: "Payments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDate",
                table: "Payments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "11111111-1111-1111-1111-111111111111",
                column: "CreatedDate",
                value: new DateTime(2025, 5, 3, 22, 18, 50, 587, DateTimeKind.Local).AddTicks(755));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "22222222-2222-2222-2222-222222222222",
                column: "CreatedDate",
                value: new DateTime(2025, 6, 3, 22, 18, 50, 587, DateTimeKind.Local).AddTicks(789));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "33333333-3333-3333-3333-333333333333",
                column: "CreatedDate",
                value: new DateTime(2025, 7, 3, 22, 18, 50, 587, DateTimeKind.Local).AddTicks(801));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "44444444-4444-4444-4444-444444444444",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 3, 22, 18, 50, 587, DateTimeKind.Local).AddTicks(816));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "55555555-5555-5555-5555-555555555555",
                column: "CreatedDate",
                value: new DateTime(2025, 9, 3, 22, 18, 50, 587, DateTimeKind.Local).AddTicks(825));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: "bbbbbbb1-bbbb-bbbb-bbbb-bbbbbbbbbbb1",
                column: "OrderDate",
                value: new DateTime(2025, 9, 23, 22, 18, 50, 587, DateTimeKind.Local).AddTicks(1565));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: "bbbbbbb2-bbbb-bbbb-bbbb-bbbbbbbbbbb2",
                column: "OrderDate",
                value: new DateTime(2025, 9, 24, 22, 18, 50, 587, DateTimeKind.Local).AddTicks(1590));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: "bbbbbbb3-bbbb-bbbb-bbbb-bbbbbbbbbbb3",
                column: "OrderDate",
                value: new DateTime(2025, 9, 25, 22, 18, 50, 587, DateTimeKind.Local).AddTicks(1656));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: "bbbbbbb4-bbbb-bbbb-bbbb-bbbbbbbbbbb4",
                column: "OrderDate",
                value: new DateTime(2025, 9, 26, 22, 18, 50, 587, DateTimeKind.Local).AddTicks(1667));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: "bbbbbbb5-bbbb-bbbb-bbbb-bbbbbbbbbbb5",
                column: "OrderDate",
                value: new DateTime(2025, 9, 27, 22, 18, 50, 587, DateTimeKind.Local).AddTicks(1676));

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentId", "Amount", "OrderId", "PaymentDate", "PaymentMethod", "Status" },
                values: new object[,]
                {
                    { "ddddddd1-dddd-dddd-dddd-dddddddddddd1", 76.00m, "bbbbbbb1-bbbb-bbbb-bbbb-bbbbbbbbbbb1", new DateTime(2025, 9, 24, 22, 18, 50, 587, DateTimeKind.Local).AddTicks(1783), "Credit Card", "Pending" },
                    { "ddddddd2-dddd-dddd-dddd-dddddddddddd2", 250.00m, "bbbbbbb2-bbbb-bbbb-bbbb-bbbbbbbbbbb2", new DateTime(2025, 9, 25, 22, 18, 50, 587, DateTimeKind.Local).AddTicks(1789), "PayPal", "Pending" },
                    { "ddddddd3-dddd-dddd-dddd-dddddddddddd3", 36.00m, "bbbbbbb3-bbbb-bbbb-bbbb-bbbbbbbbbbb3", new DateTime(2025, 9, 26, 22, 18, 50, 587, DateTimeKind.Local).AddTicks(1795), "Credit Card", "Pending" },
                    { "ddddddd4-dddd-dddd-dddd-dddddddddddd4", 40.00m, "bbbbbbb5-bbbb-bbbb-bbbb-bbbbbbbbbbb5", new DateTime(2025, 9, 28, 22, 18, 50, 587, DateTimeKind.Local).AddTicks(1800), "Debit Card", "Pending" },
                    { "ddddddd5-dddd-dddd-dddd-dddddddddddd5", 0.00m, "bbbbbbb4-bbbb-bbbb-bbbb-bbbbbbbbbbb4", new DateTime(2025, 9, 27, 22, 18, 50, 587, DateTimeKind.Local).AddTicks(1806), "N/A", "Pending" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OrderId",
                table: "Payments",
                column: "OrderId",
                unique: true,
                filter: "[OrderId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Orders_OrderId",
                table: "Payments",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId");
        }
    }
}
