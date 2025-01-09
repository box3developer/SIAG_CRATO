using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIAG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Teste2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_areaarmazenagem_agrupadorativo_id_agrupadorreservado",
                table: "areaarmazenagem");

            migrationBuilder.RenameColumn(
                name: "id_agrupadorreservado",
                table: "areaarmazenagem",
                newName: "id_agrupador_reservado");

            migrationBuilder.RenameIndex(
                name: "IX_areaarmazenagem_id_agrupadorreservado",
                table: "areaarmazenagem",
                newName: "IX_areaarmazenagem_id_agrupador_reservado");

            migrationBuilder.AddForeignKey(
                name: "FK_areaarmazenagem_agrupadorativo_id_agrupador_reservado",
                table: "areaarmazenagem",
                column: "id_agrupador_reservado",
                principalTable: "agrupadorativo",
                principalColumn: "id_agrupador");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_areaarmazenagem_agrupadorativo_id_agrupador_reservado",
                table: "areaarmazenagem");

            migrationBuilder.RenameColumn(
                name: "id_agrupador_reservado",
                table: "areaarmazenagem",
                newName: "id_agrupadorreservado");

            migrationBuilder.RenameIndex(
                name: "IX_areaarmazenagem_id_agrupador_reservado",
                table: "areaarmazenagem",
                newName: "IX_areaarmazenagem_id_agrupadorreservado");

            migrationBuilder.AddForeignKey(
                name: "FK_areaarmazenagem_agrupadorativo_id_agrupadorreservado",
                table: "areaarmazenagem",
                column: "id_agrupadorreservado",
                principalTable: "agrupadorativo",
                principalColumn: "id_agrupador");
        }
    }
}
