using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_APP.Migrations
{
    /// <inheritdoc />
    public partial class tripinfoupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DriverNAme",
                table: "Trip",
                newName: "DriverName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DriverName",
                table: "Trip",
                newName: "DriverNAme");
        }
    }
}
