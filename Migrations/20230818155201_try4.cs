using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_APP.Migrations
{
    /// <inheritdoc />
    public partial class try4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "AspNetUsers",
                newName: "LastName");

            migrationBuilder.UpdateData(
                table: "Trip",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DeliveryDate", "PickupDate" },
                values: new object[] { new DateTime(2023, 8, 19, 11, 52, 0, 729, DateTimeKind.Local).AddTicks(7921), new DateTime(2023, 8, 18, 11, 52, 0, 729, DateTimeKind.Local).AddTicks(7871) });

            migrationBuilder.UpdateData(
                table: "Trip",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DeliveryDate", "PickupDate" },
                values: new object[] { new DateTime(2023, 8, 21, 11, 52, 0, 729, DateTimeKind.Local).AddTicks(7936), new DateTime(2023, 8, 20, 11, 52, 0, 729, DateTimeKind.Local).AddTicks(7933) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "AspNetUsers",
                newName: "lastName");

            migrationBuilder.UpdateData(
                table: "Trip",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DeliveryDate", "PickupDate" },
                values: new object[] { new DateTime(2023, 8, 18, 21, 48, 58, 972, DateTimeKind.Local).AddTicks(313), new DateTime(2023, 8, 17, 21, 48, 58, 972, DateTimeKind.Local).AddTicks(251) });

            migrationBuilder.UpdateData(
                table: "Trip",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DeliveryDate", "PickupDate" },
                values: new object[] { new DateTime(2023, 8, 20, 21, 48, 58, 972, DateTimeKind.Local).AddTicks(329), new DateTime(2023, 8, 19, 21, 48, 58, 972, DateTimeKind.Local).AddTicks(326) });
        }
    }
}
