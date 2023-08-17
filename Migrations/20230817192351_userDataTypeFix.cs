using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_APP.Migrations
{
    /// <inheritdoc />
    public partial class userDataTypeFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "PayRate",
                table: "AspNetUsers",
                type: "float",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<bool>(
                name: "Availability",
                table: "AspNetUsers",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.UpdateData(
                table: "Trip",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DeliveryDate", "PickupDate" },
                values: new object[] { new DateTime(2023, 8, 18, 15, 23, 51, 98, DateTimeKind.Local).AddTicks(7789), new DateTime(2023, 8, 17, 15, 23, 51, 98, DateTimeKind.Local).AddTicks(7719) });

            migrationBuilder.UpdateData(
                table: "Trip",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DeliveryDate", "PickupDate" },
                values: new object[] { new DateTime(2023, 8, 20, 15, 23, 51, 98, DateTimeKind.Local).AddTicks(7811), new DateTime(2023, 8, 19, 15, 23, 51, 98, DateTimeKind.Local).AddTicks(7807) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PayRate",
                table: "AspNetUsers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Availability",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Trip",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DeliveryDate", "PickupDate" },
                values: new object[] { new DateTime(2023, 8, 18, 15, 4, 56, 780, DateTimeKind.Local).AddTicks(4868), new DateTime(2023, 8, 17, 15, 4, 56, 780, DateTimeKind.Local).AddTicks(4821) });

            migrationBuilder.UpdateData(
                table: "Trip",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DeliveryDate", "PickupDate" },
                values: new object[] { new DateTime(2023, 8, 20, 15, 4, 56, 780, DateTimeKind.Local).AddTicks(4880), new DateTime(2023, 8, 19, 15, 4, 56, 780, DateTimeKind.Local).AddTicks(4878) });
        }
    }
}
