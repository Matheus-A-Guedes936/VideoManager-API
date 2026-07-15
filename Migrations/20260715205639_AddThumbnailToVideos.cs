using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoManager_API.Migrations
{
    /// <inheritdoc />
    public partial class AddThumbnailToVideos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CaminhoVideoThumbnail",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaminhoVideoThumbnail",
                table: "Videos");
        }
    }
}
