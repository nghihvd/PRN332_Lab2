using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRN232.Lab2.CoffeeStore.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Products",
                type: "datetime2",
                nullable: true);

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

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "aaaaaaa1-aaaa-aaaa-aaaa-aaaaaaaaaaa1",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "aaaaaaa2-aaaa-aaaa-aaaa-aaaaaaaaaaa2",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "aaaaaaa3-aaaa-aaaa-aaaa-aaaaaaaaaaa3",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "aaaaaaa4-aaaa-aaaa-aaaa-aaaaaaaaaaa4",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "aaaaaaa5-aaaa-aaaa-aaaa-aaaaaaaaaaa5",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "11111111-1111-1111-1111-111111111111",
                column: "CreatedDate",
                value: new DateTime(2025, 4, 26, 15, 26, 0, 651, DateTimeKind.Local).AddTicks(1130));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "22222222-2222-2222-2222-222222222222",
                column: "CreatedDate",
                value: new DateTime(2025, 5, 26, 15, 26, 0, 651, DateTimeKind.Local).AddTicks(1155));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "33333333-3333-3333-3333-333333333333",
                column: "CreatedDate",
                value: new DateTime(2025, 6, 26, 15, 26, 0, 651, DateTimeKind.Local).AddTicks(1161));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "44444444-4444-4444-4444-444444444444",
                column: "CreatedDate",
                value: new DateTime(2025, 7, 26, 15, 26, 0, 651, DateTimeKind.Local).AddTicks(1173));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "55555555-5555-5555-5555-555555555555",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 15, 26, 0, 651, DateTimeKind.Local).AddTicks(1178));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: "bbbbbbb1-bbbb-bbbb-bbbb-bbbbbbbbbbb1",
                column: "OrderDate",
                value: new DateTime(2025, 9, 16, 15, 26, 0, 651, DateTimeKind.Local).AddTicks(1350));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: "bbbbbbb2-bbbb-bbbb-bbbb-bbbbbbbbbbb2",
                column: "OrderDate",
                value: new DateTime(2025, 9, 17, 15, 26, 0, 651, DateTimeKind.Local).AddTicks(1367));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: "bbbbbbb3-bbbb-bbbb-bbbb-bbbbbbbbbbb3",
                column: "OrderDate",
                value: new DateTime(2025, 9, 18, 15, 26, 0, 651, DateTimeKind.Local).AddTicks(1373));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: "bbbbbbb4-bbbb-bbbb-bbbb-bbbbbbbbbbb4",
                column: "OrderDate",
                value: new DateTime(2025, 9, 19, 15, 26, 0, 651, DateTimeKind.Local).AddTicks(1380));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: "bbbbbbb5-bbbb-bbbb-bbbb-bbbbbbbbbbb5",
                column: "OrderDate",
                value: new DateTime(2025, 9, 20, 15, 26, 0, 651, DateTimeKind.Local).AddTicks(1386));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: "ddddddd1-dddd-dddd-dddd-dddddddddddd1",
                column: "PaymentDate",
                value: new DateTime(2025, 9, 17, 15, 26, 0, 651, DateTimeKind.Local).AddTicks(1445));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: "ddddddd2-dddd-dddd-dddd-dddddddddddd2",
                column: "PaymentDate",
                value: new DateTime(2025, 9, 18, 15, 26, 0, 651, DateTimeKind.Local).AddTicks(1449));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: "ddddddd3-dddd-dddd-dddd-dddddddddddd3",
                column: "PaymentDate",
                value: new DateTime(2025, 9, 19, 15, 26, 0, 651, DateTimeKind.Local).AddTicks(1453));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: "ddddddd4-dddd-dddd-dddd-dddddddddddd4",
                column: "PaymentDate",
                value: new DateTime(2025, 9, 21, 15, 26, 0, 651, DateTimeKind.Local).AddTicks(1457));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: "ddddddd5-dddd-dddd-dddd-dddddddddddd5",
                column: "PaymentDate",
                value: new DateTime(2025, 9, 20, 15, 26, 0, 651, DateTimeKind.Local).AddTicks(1461));
        }
    }
}
