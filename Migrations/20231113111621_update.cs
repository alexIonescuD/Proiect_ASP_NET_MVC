using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proiect_ASP_NET_MVC.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PublishedBook_Car_CarID",
                table: "PublishedBook");

            migrationBuilder.DropForeignKey(
                name: "FK_PublishedBook_Publisher_DealerID",
                table: "PublishedBook");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Publisher",
                table: "Publisher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PublishedBook",
                table: "PublishedBook");

            migrationBuilder.RenameTable(
                name: "Publisher",
                newName: "Dealer");

            migrationBuilder.RenameTable(
                name: "PublishedBook",
                newName: "SoldBook");

            migrationBuilder.RenameIndex(
                name: "IX_PublishedBook_DealerID",
                table: "SoldBook",
                newName: "IX_SoldBook_DealerID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dealer",
                table: "Dealer",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SoldBook",
                table: "SoldBook",
                columns: new[] { "CarID", "DealerID" });

            migrationBuilder.AddForeignKey(
                name: "FK_SoldBook_Car_CarID",
                table: "SoldBook",
                column: "CarID",
                principalTable: "Car",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SoldBook_Dealer_DealerID",
                table: "SoldBook",
                column: "DealerID",
                principalTable: "Dealer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SoldBook_Car_CarID",
                table: "SoldBook");

            migrationBuilder.DropForeignKey(
                name: "FK_SoldBook_Dealer_DealerID",
                table: "SoldBook");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SoldBook",
                table: "SoldBook");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dealer",
                table: "Dealer");

            migrationBuilder.RenameTable(
                name: "SoldBook",
                newName: "PublishedBook");

            migrationBuilder.RenameTable(
                name: "Dealer",
                newName: "Publisher");

            migrationBuilder.RenameIndex(
                name: "IX_SoldBook_DealerID",
                table: "PublishedBook",
                newName: "IX_PublishedBook_DealerID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PublishedBook",
                table: "PublishedBook",
                columns: new[] { "CarID", "DealerID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Publisher",
                table: "Publisher",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PublishedBook_Car_CarID",
                table: "PublishedBook",
                column: "CarID",
                principalTable: "Car",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PublishedBook_Publisher_DealerID",
                table: "PublishedBook",
                column: "DealerID",
                principalTable: "Publisher",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
