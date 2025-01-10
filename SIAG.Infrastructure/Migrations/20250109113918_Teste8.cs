using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIAG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Teste8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "id_endereco",
                table: "operadorhistorico",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_endereco",
                table: "equipamento",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_endereco",
                table: "endereco",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "id_endereco",
                table: "caixaleitura",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_endereco",
                table: "areaarmazenagem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_endereco",
                table: "endereco",
                column: "id_endereco");

            migrationBuilder.CreateIndex(
                name: "IX_operadorhistorico_id_endereco",
                table: "operadorhistorico",
                column: "id_endereco");

            migrationBuilder.CreateIndex(
                name: "IX_equipamento_id_endereco",
                table: "equipamento",
                column: "id_endereco");

            migrationBuilder.CreateIndex(
                name: "IX_caixaleitura_id_endereco",
                table: "caixaleitura",
                column: "id_endereco");

            migrationBuilder.CreateIndex(
                name: "IX_areaarmazenagem_id_endereco",
                table: "areaarmazenagem",
                column: "id_endereco");

            migrationBuilder.AddForeignKey(
                name: "FK_areaarmazenagem_endereco_id_endereco",
                table: "areaarmazenagem",
                column: "id_endereco",
                principalTable: "endereco",
                principalColumn: "id_endereco",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_caixaleitura_endereco_id_endereco",
                table: "caixaleitura",
                column: "id_endereco",
                principalTable: "endereco",
                principalColumn: "id_endereco");

            migrationBuilder.AddForeignKey(
                name: "FK_equipamento_endereco_id_endereco",
                table: "equipamento",
                column: "id_endereco",
                principalTable: "endereco",
                principalColumn: "id_endereco");

            migrationBuilder.AddForeignKey(
                name: "FK_operadorhistorico_endereco_id_endereco",
                table: "operadorhistorico",
                column: "id_endereco",
                principalTable: "endereco",
                principalColumn: "id_endereco");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_areaarmazenagem_endereco_id_endereco",
                table: "areaarmazenagem");

            migrationBuilder.DropForeignKey(
                name: "FK_caixaleitura_endereco_id_endereco",
                table: "caixaleitura");

            migrationBuilder.DropForeignKey(
                name: "FK_equipamento_endereco_id_endereco",
                table: "equipamento");

            migrationBuilder.DropForeignKey(
                name: "FK_operadorhistorico_endereco_id_endereco",
                table: "operadorhistorico");

            migrationBuilder.DropIndex(
                name: "IX_operadorhistorico_id_endereco",
                table: "operadorhistorico");

            migrationBuilder.DropIndex(
                name: "IX_equipamento_id_endereco",
                table: "equipamento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_endereco",
                table: "endereco");

            migrationBuilder.DropIndex(
                name: "IX_caixaleitura_id_endereco",
                table: "caixaleitura");

            migrationBuilder.DropIndex(
                name: "IX_areaarmazenagem_id_endereco",
                table: "areaarmazenagem");

            migrationBuilder.DropColumn(
                name: "id_endereco",
                table: "operadorhistorico");

            migrationBuilder.DropColumn(
                name: "id_endereco",
                table: "equipamento");

            migrationBuilder.DropColumn(
                name: "id_endereco",
                table: "endereco");

            migrationBuilder.DropColumn(
                name: "id_endereco",
                table: "caixaleitura");

            migrationBuilder.DropColumn(
                name: "id_endereco",
                table: "areaarmazenagem");
        }
    }
}
