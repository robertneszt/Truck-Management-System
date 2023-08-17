using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_APP.Migrations
{
    /// <inheritdoc />
    public partial class updateUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PayRate",
                table: "AspNetUsers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "PayRate",
                table: "AspNetUsers",
                type: "decimal(18,2)",
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

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
    }
}
