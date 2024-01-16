using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proiect_ASP_NET_MVC.Migrations
{
    /// <inheritdoc />
    public partial class engineAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Engine",
                table: "Car",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Engine",
                table: "Car");
        }
    }
}
