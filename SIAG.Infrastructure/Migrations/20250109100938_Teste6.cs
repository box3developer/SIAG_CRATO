using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIAG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Teste6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_caixa_pallet_id_pedido",
                table: "caixa");

            migrationBuilder.AddForeignKey(
                name: "FK_caixa_pedido_id_pedido",
                table: "caixa",
                column: "id_pedido",
                principalTable: "pedido",
                principalColumn: "id_pedido");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_caixa_pedido_id_pedido",
                table: "caixa");

            migrationBuilder.AddForeignKey(
                name: "FK_caixa_pallet_id_pedido",
                table: "caixa",
                column: "id_pedido",
                principalTable: "pallet",
                principalColumn: "id_pallet");
        }
    }
}
