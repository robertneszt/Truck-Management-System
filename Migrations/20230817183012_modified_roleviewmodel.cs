using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_APP.Migrations
{
    /// <inheritdoc />
    public partial class modified_roleviewmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Trip",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DeliveryDate", "PickupDate" },
                values: new object[] { new DateTime(2023, 8, 18, 14, 30, 12, 74, DateTimeKind.Local).AddTicks(5183), new DateTime(2023, 8, 17, 14, 30, 12, 74, DateTimeKind.Local).AddTicks(5130) });

            migrationBuilder.UpdateData(
                table: "Trip",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DeliveryDate", "PickupDate" },
                values: new object[] { new DateTime(2023, 8, 20, 14, 30, 12, 74, DateTimeKind.Local).AddTicks(5194), new DateTime(2023, 8, 19, 14, 30, 12, 74, DateTimeKind.Local).AddTicks(5191) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Trip",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DeliveryDate", "PickupDate" },
                values: new object[] { new DateTime(2023, 8, 18, 13, 3, 6, 264, DateTimeKind.Local).AddTicks(3298), new DateTime(2023, 8, 17, 13, 3, 6, 264, DateTimeKind.Local).AddTicks(3259) });

            migrationBuilder.UpdateData(
                table: "Trip",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DeliveryDate", "PickupDate" },
                values: new object[] { new DateTime(2023, 8, 20, 13, 3, 6, 264, DateTimeKind.Local).AddTicks(3311), new DateTime(2023, 8, 19, 13, 3, 6, 264, DateTimeKind.Local).AddTicks(3309) });
        }
    }
}
