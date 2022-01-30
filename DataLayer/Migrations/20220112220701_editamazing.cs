using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class editamazing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Amazing_Suggest_ProductId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Amazing_Suggest",
                table: "Amazing_Suggest");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Amazing_Suggest",
                newName: "Amazing_Suggests");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Amazing_Suggests",
                table: "Amazing_Suggests",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Amazing_Suggests_ProductId",
                table: "Amazing_Suggests",
                column: "ProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Amazing_Suggests_Products_ProductId",
                table: "Amazing_Suggests",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Amazing_Suggests_Products_ProductId",
                table: "Amazing_Suggests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Amazing_Suggests",
                table: "Amazing_Suggests");

            migrationBuilder.DropIndex(
                name: "IX_Amazing_Suggests_ProductId",
                table: "Amazing_Suggests");

            migrationBuilder.RenameTable(
                name: "Amazing_Suggests",
                newName: "Amazing_Suggest");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Amazing_Suggest",
                table: "Amazing_Suggest",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductId",
                table: "Products",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Amazing_Suggest_ProductId",
                table: "Products",
                column: "ProductId",
                principalTable: "Amazing_Suggest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
