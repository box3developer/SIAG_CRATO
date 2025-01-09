using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIAG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Teste4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "id_programa",
                table: "programa",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "id_pedido",
                table: "pedido",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "id_pedido",
                table: "caixa",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_programa",
                table: "caixa",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_programa",
                table: "programa",
                column: "id_programa");

            migrationBuilder.AddPrimaryKey(
                name: "PK_pedido",
                table: "pedido",
                column: "id_pedido");

            migrationBuilder.CreateIndex(
                name: "IX_caixa_id_pedido",
                table: "caixa",
                column: "id_pedido");

            migrationBuilder.CreateIndex(
                name: "IX_caixa_id_programa",
                table: "caixa",
                column: "id_programa");

            migrationBuilder.AddForeignKey(
                name: "FK_caixa_pallet_id_pedido",
                table: "caixa",
                column: "id_pedido",
                principalTable: "pallet",
                principalColumn: "id_pallet");

            migrationBuilder.AddForeignKey(
                name: "FK_caixa_programa_id_programa",
                table: "caixa",
                column: "id_programa",
                principalTable: "programa",
                principalColumn: "id_programa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_caixa_pallet_id_pedido",
                table: "caixa");

            migrationBuilder.DropForeignKey(
                name: "FK_caixa_programa_id_programa",
                table: "caixa");

            migrationBuilder.DropPrimaryKey(
                name: "PK_programa",
                table: "programa");

            migrationBuilder.DropPrimaryKey(
                name: "PK_pedido",
                table: "pedido");

            migrationBuilder.DropIndex(
                name: "IX_caixa_id_pedido",
                table: "caixa");

            migrationBuilder.DropIndex(
                name: "IX_caixa_id_programa",
                table: "caixa");

            migrationBuilder.DropColumn(
                name: "id_programa",
                table: "programa");

            migrationBuilder.DropColumn(
                name: "id_pedido",
                table: "pedido");

            migrationBuilder.DropColumn(
                name: "id_pedido",
                table: "caixa");

            migrationBuilder.DropColumn(
                name: "id_programa",
                table: "caixa");
        }
    }
}
