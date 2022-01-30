using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class ProductsOfGroupsIIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "GroupProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "productsOfGroupsIIndices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productsOfGroupsIIndices", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupProducts_GroupId",
                table: "GroupProducts",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupProducts_productsOfGroupsIIndices_GroupId",
                table: "GroupProducts",
                column: "GroupId",
                principalTable: "productsOfGroupsIIndices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupProducts_productsOfGroupsIIndices_GroupId",
                table: "GroupProducts");

            migrationBuilder.DropTable(
                name: "productsOfGroupsIIndices");

            migrationBuilder.DropIndex(
                name: "IX_GroupProducts_GroupId",
                table: "GroupProducts");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "GroupProducts");
        }
    }
}
