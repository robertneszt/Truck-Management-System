using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_APP.Migrations
{
    /// <inheritdoc />
    public partial class driverListView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                values: new object[] { new DateTime(2023, 8, 18, 22, 54, 23, 304, DateTimeKind.Local).AddTicks(2805), new DateTime(2023, 8, 17, 22, 54, 23, 304, DateTimeKind.Local).AddTicks(2751) });

            migrationBuilder.UpdateData(
                table: "Trip",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DeliveryDate", "PickupDate" },
                values: new object[] { new DateTime(2023, 8, 20, 22, 54, 23, 304, DateTimeKind.Local).AddTicks(2819), new DateTime(2023, 8, 19, 22, 54, 23, 304, DateTimeKind.Local).AddTicks(2817) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
