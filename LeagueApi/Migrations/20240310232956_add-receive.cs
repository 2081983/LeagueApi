using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeagueApi.Migrations
{
    /// <inheritdoc />
    public partial class addreceive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Receive",
                table: "Results",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Receive",
                table: "Results");
        }
    }
}
