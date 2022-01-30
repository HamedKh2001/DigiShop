using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class EventBanner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BannerPhotos_Events_BannerID",
                table: "BannerPhotos");

            migrationBuilder.RenameColumn(
                name: "BannerID",
                table: "BannerPhotos",
                newName: "EventID");

            migrationBuilder.RenameIndex(
                name: "IX_BannerPhotos_BannerID",
                table: "BannerPhotos",
                newName: "IX_BannerPhotos_EventID");

            migrationBuilder.AddForeignKey(
                name: "FK_BannerPhotos_Events_EventID",
                table: "BannerPhotos",
                column: "EventID",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BannerPhotos_Events_EventID",
                table: "BannerPhotos");

            migrationBuilder.RenameColumn(
                name: "EventID",
                table: "BannerPhotos",
                newName: "BannerID");

            migrationBuilder.RenameIndex(
                name: "IX_BannerPhotos_EventID",
                table: "BannerPhotos",
                newName: "IX_BannerPhotos_BannerID");

            migrationBuilder.AddForeignKey(
                name: "FK_BannerPhotos_Events_BannerID",
                table: "BannerPhotos",
                column: "BannerID",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
