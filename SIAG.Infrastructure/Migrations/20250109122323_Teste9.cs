using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIAG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Teste9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_operadorhistorico_operador_id_operador",
                table: "operadorhistorico");

            migrationBuilder.DropIndex(
                name: "IX_operadorhistorico_id_operador",
                table: "operadorhistorico");

            migrationBuilder.DropColumn(
                name: "id_operador",
                table: "operadorhistorico");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "id_operador",
                table: "operadorhistorico",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_operadorhistorico_id_operador",
                table: "operadorhistorico",
                column: "id_operador");

            migrationBuilder.AddForeignKey(
                name: "FK_operadorhistorico_operador_id_operador",
                table: "operadorhistorico",
                column: "id_operador",
                principalTable: "operador",
                principalColumn: "id_operador");
        }
    }
}
