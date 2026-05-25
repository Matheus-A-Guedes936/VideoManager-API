using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoManager_API.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoUsuarioIdNoVideosModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Usuarios_UsuarioId",
                table: "Videos");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Videos",
                newName: "UsuarioID");

            migrationBuilder.RenameIndex(
                name: "IX_Videos_UsuarioId",
                table: "Videos",
                newName: "IX_Videos_UsuarioID");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Usuarios_UsuarioID",
                table: "Videos",
                column: "UsuarioID",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Usuarios_UsuarioID",
                table: "Videos");

            migrationBuilder.RenameColumn(
                name: "UsuarioID",
                table: "Videos",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Videos_UsuarioID",
                table: "Videos",
                newName: "IX_Videos_UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Usuarios_UsuarioId",
                table: "Videos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
