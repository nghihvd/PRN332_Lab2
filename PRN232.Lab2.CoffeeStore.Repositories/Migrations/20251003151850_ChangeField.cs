using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRN232.Lab2.CoffeeStore.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class ChangeField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Payments_OrderId",
                table: "Payments");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

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
                columns: new[] { "OrderDate", "TotalAmount" },
                values: new object[] { new DateTime(2025, 9, 23, 22, 18, 50, 587, DateTimeKind.Local).AddTicks(1565), 0m });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: "bbbbbbb2-bbbb-bbbb-bbbb-bbbbbbbbbbb2",
                columns: new[] { "OrderDate", "TotalAmount" },
                values: new object[] { new DateTime(2025, 9, 24, 22, 18, 50, 587, DateTimeKind.Local).AddTicks(1590), 0m });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: "bbbbbbb3-bbbb-bbbb-bbbb-bbbbbbbbbbb3",
                columns: new[] { "OrderDate", "TotalAmount" },
                values: new object[] { new DateTime(2025, 9, 25, 22, 18, 50, 587, DateTimeKind.Local).AddTicks(1656), 0m });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: "bbbbbbb4-bbbb-bbbb-bbbb-bbbbbbbbbbb4",
                columns: new[] { "OrderDate", "TotalAmount" },
                values: new object[] { new DateTime(2025, 9, 26, 22, 18, 50, 587, DateTimeKind.Local).AddTicks(1667), 0m });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: "bbbbbbb5-bbbb-bbbb-bbbb-bbbbbbbbbbb5",
                columns: new[] { "OrderDate", "TotalAmount" },
                values: new object[] { new DateTime(2025, 9, 27, 22, 18, 50, 587, DateTimeKind.Local).AddTicks(1676), 0m });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: "ddddddd1-dddd-dddd-dddd-dddddddddddd1",
                columns: new[] { "PaymentDate", "Status" },
                values: new object[] { new DateTime(2025, 9, 24, 22, 18, 50, 587, DateTimeKind.Local).AddTicks(1783), "Pending" });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: "ddddddd2-dddd-dddd-dddd-dddddddddddd2",
                columns: new[] { "PaymentDate", "Status" },
                values: new object[] { new DateTime(2025, 9, 25, 22, 18, 50, 587, DateTimeKind.Local).AddTicks(1789), "Pending" });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: "ddddddd3-dddd-dddd-dddd-dddddddddddd3",
                columns: new[] { "PaymentDate", "Status" },
                values: new object[] { new DateTime(2025, 9, 26, 22, 18, 50, 587, DateTimeKind.Local).AddTicks(1795), "Pending" });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: "ddddddd4-dddd-dddd-dddd-dddddddddddd4",
                columns: new[] { "PaymentDate", "Status" },
                values: new object[] { new DateTime(2025, 9, 28, 22, 18, 50, 587, DateTimeKind.Local).AddTicks(1800), "Pending" });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: "ddddddd5-dddd-dddd-dddd-dddddddddddd5",
                columns: new[] { "PaymentDate", "Status" },
                values: new object[] { new DateTime(2025, 9, 27, 22, 18, 50, 587, DateTimeKind.Local).AddTicks(1806), "Pending" });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OrderId",
                table: "Payments",
                column: "OrderId",
                unique: true,
                filter: "[OrderId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Payments_OrderId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "11111111-1111-1111-1111-111111111111",
                column: "CreatedDate",
                value: new DateTime(2025, 5, 3, 15, 49, 23, 835, DateTimeKind.Local).AddTicks(8927));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "22222222-2222-2222-2222-222222222222",
                column: "CreatedDate",
                value: new DateTime(2025, 6, 3, 15, 49, 23, 835, DateTimeKind.Local).AddTicks(8958));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "33333333-3333-3333-3333-333333333333",
                column: "CreatedDate",
                value: new DateTime(2025, 7, 3, 15, 49, 23, 835, DateTimeKind.Local).AddTicks(8964));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "44444444-4444-4444-4444-444444444444",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 3, 15, 49, 23, 835, DateTimeKind.Local).AddTicks(8970));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "55555555-5555-5555-5555-555555555555",
                column: "CreatedDate",
                value: new DateTime(2025, 9, 3, 15, 49, 23, 835, DateTimeKind.Local).AddTicks(8976));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: "bbbbbbb1-bbbb-bbbb-bbbb-bbbbbbbbbbb1",
                column: "OrderDate",
                value: new DateTime(2025, 9, 23, 15, 49, 23, 835, DateTimeKind.Local).AddTicks(9166));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: "bbbbbbb2-bbbb-bbbb-bbbb-bbbbbbbbbbb2",
                column: "OrderDate",
                value: new DateTime(2025, 9, 24, 15, 49, 23, 835, DateTimeKind.Local).AddTicks(9181));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: "bbbbbbb3-bbbb-bbbb-bbbb-bbbbbbbbbbb3",
                column: "OrderDate",
                value: new DateTime(2025, 9, 25, 15, 49, 23, 835, DateTimeKind.Local).AddTicks(9187));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: "bbbbbbb4-bbbb-bbbb-bbbb-bbbbbbbbbbb4",
                column: "OrderDate",
                value: new DateTime(2025, 9, 26, 15, 49, 23, 835, DateTimeKind.Local).AddTicks(9194));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: "bbbbbbb5-bbbb-bbbb-bbbb-bbbbbbbbbbb5",
                column: "OrderDate",
                value: new DateTime(2025, 9, 27, 15, 49, 23, 835, DateTimeKind.Local).AddTicks(9200));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: "ddddddd1-dddd-dddd-dddd-dddddddddddd1",
                column: "PaymentDate",
                value: new DateTime(2025, 9, 24, 15, 49, 23, 835, DateTimeKind.Local).AddTicks(9261));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: "ddddddd2-dddd-dddd-dddd-dddddddddddd2",
                column: "PaymentDate",
                value: new DateTime(2025, 9, 25, 15, 49, 23, 835, DateTimeKind.Local).AddTicks(9265));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: "ddddddd3-dddd-dddd-dddd-dddddddddddd3",
                column: "PaymentDate",
                value: new DateTime(2025, 9, 26, 15, 49, 23, 835, DateTimeKind.Local).AddTicks(9269));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: "ddddddd4-dddd-dddd-dddd-dddddddddddd4",
                column: "PaymentDate",
                value: new DateTime(2025, 9, 28, 15, 49, 23, 835, DateTimeKind.Local).AddTicks(9273));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: "ddddddd5-dddd-dddd-dddd-dddddddddddd5",
                column: "PaymentDate",
                value: new DateTime(2025, 9, 27, 15, 49, 23, 835, DateTimeKind.Local).AddTicks(9277));

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OrderId",
                table: "Payments",
                column: "OrderId");
        }
    }
}
