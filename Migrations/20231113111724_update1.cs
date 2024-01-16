using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proiect_ASP_NET_MVC.Migrations
{
    /// <inheritdoc />
    public partial class update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameTable(
                name: "SoldBook",
                newName: "SoldCar");

            migrationBuilder.RenameIndex(
                name: "IX_SoldBook_DealerID",
                table: "SoldCar",
                newName: "IX_SoldCar_DealerID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SoldCar",
                table: "SoldCar",
                columns: new[] { "CarID", "DealerID" });

            migrationBuilder.AddForeignKey(
                name: "FK_SoldCar_Car_CarID",
                table: "SoldCar",
                column: "CarID",
                principalTable: "Car",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SoldCar_Dealer_DealerID",
                table: "SoldCar",
                column: "DealerID",
                principalTable: "Dealer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SoldCar_Car_CarID",
                table: "SoldCar");

            migrationBuilder.DropForeignKey(
                name: "FK_SoldCar_Dealer_DealerID",
                table: "SoldCar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SoldCar",
                table: "SoldCar");

            migrationBuilder.RenameTable(
                name: "SoldCar",
                newName: "SoldBook");

            migrationBuilder.RenameIndex(
                name: "IX_SoldCar_DealerID",
                table: "SoldBook",
                newName: "IX_SoldBook_DealerID");

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
    }
}
