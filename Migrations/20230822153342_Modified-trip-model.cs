using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_APP.Migrations
{
    /// <inheritdoc />
    public partial class Modifiedtripmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "EstimateDistance",
                table: "Trip",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Pay",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PayDate",
                table: "Pay",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Pay",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimateDistance",
                table: "Trip");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Pay");

            migrationBuilder.DropColumn(
                name: "PayDate",
                table: "Pay");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Pay");
        }
    }
}
