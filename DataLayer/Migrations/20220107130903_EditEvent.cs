using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class EditEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BannerPhotos");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Events",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Events");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BannerPhotos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
