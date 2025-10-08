using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRN232.Lab2.CoffeeStore.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class ChangeA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "11111111-1111-1111-1111-111111111111",
                column: "CreatedDate",
                value: new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "22222222-2222-2222-2222-222222222222",
                column: "CreatedDate",
                value: new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "33333333-3333-3333-3333-333333333333",
                column: "CreatedDate",
                value: new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "44444444-4444-4444-4444-444444444444",
                column: "CreatedDate",
                value: new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "55555555-5555-5555-5555-555555555555",
                column: "CreatedDate",
                value: new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: "bbbbbbb1-bbbb-bbbb-bbbb-bbbbbbbbbbb1",
                column: "OrderDate",
                value: new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: "bbbbbbb2-bbbb-bbbb-bbbb-bbbbbbbbbbb2",
                column: "OrderDate",
                value: new DateTime(2025, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: "bbbbbbb3-bbbb-bbbb-bbbb-bbbbbbbbbbb3",
                column: "OrderDate",
                value: new DateTime(2025, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: "bbbbbbb4-bbbb-bbbb-bbbb-bbbbbbbbbbb4",
                column: "OrderDate",
                value: new DateTime(2025, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: "bbbbbbb5-bbbb-bbbb-bbbb-bbbbbbbbbbb5",
                column: "OrderDate",
                value: new DateTime(2025, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                column: "OrderDate",
                value: new DateTime(2025, 9, 28, 20, 49, 36, 701, DateTimeKind.Local).AddTicks(3336));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: "bbbbbbb2-bbbb-bbbb-bbbb-bbbbbbbbbbb2",
                column: "OrderDate",
                value: new DateTime(2025, 9, 29, 20, 49, 36, 701, DateTimeKind.Local).AddTicks(3594));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: "bbbbbbb3-bbbb-bbbb-bbbb-bbbbbbbbbbb3",
                column: "OrderDate",
                value: new DateTime(2025, 9, 30, 20, 49, 36, 701, DateTimeKind.Local).AddTicks(3603));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: "bbbbbbb4-bbbb-bbbb-bbbb-bbbbbbbbbbb4",
                column: "OrderDate",
                value: new DateTime(2025, 10, 1, 20, 49, 36, 701, DateTimeKind.Local).AddTicks(3610));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: "bbbbbbb5-bbbb-bbbb-bbbb-bbbbbbbbbbb5",
                column: "OrderDate",
                value: new DateTime(2025, 10, 2, 20, 49, 36, 701, DateTimeKind.Local).AddTicks(3618));
        }
    }
}
