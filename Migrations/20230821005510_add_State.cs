using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_APP.Migrations
{
    /// <inheritdoc />
    public partial class add_State : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliveryLocationState",
                table: "Trip",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PickupLocationState",
                table: "Trip",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryLocationState",
                table: "Trip");

            migrationBuilder.DropColumn(
                name: "PickupLocationState",
                table: "Trip");
        }
    }
}
