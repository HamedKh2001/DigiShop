using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class EDitEvent2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BannerPhotos_EventID",
                table: "BannerPhotos");

            migrationBuilder.CreateIndex(
                name: "IX_BannerPhotos_EventID",
                table: "BannerPhotos",
                column: "EventID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BannerPhotos_EventID",
                table: "BannerPhotos");

            migrationBuilder.CreateIndex(
                name: "IX_BannerPhotos_EventID",
                table: "BannerPhotos",
                column: "EventID");
        }
    }
}
