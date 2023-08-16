using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_APP.Migrations
{
    /// <inheritdoc />
    public partial class tripinfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DriverNAme",
                table: "Trip",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DriverNAme",
                table: "Trip");
        }
    }
}
