using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIAG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoTabelaChecklistOperador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_equipamentochecklistoperador2_equipamento_id_equipamento",
                table: "equipamentochecklistoperador2");

            migrationBuilder.DropForeignKey(
                name: "FK_equipamentochecklistoperador2_equipamentochecklist_id_equipamentochecklist",
                table: "equipamentochecklistoperador2");

            migrationBuilder.DropForeignKey(
                name: "FK_equipamentochecklistoperador2_operador_id_operador",
                table: "equipamentochecklistoperador2");

            migrationBuilder.DropPrimaryKey(
                name: "PK_equipamentochecklistoperador2",
                table: "equipamentochecklistoperador2");

            migrationBuilder.RenameTable(
                name: "equipamentochecklistoperador2",
                newName: "equipamentochecklistoperador");

            migrationBuilder.RenameIndex(
                name: "IX_equipamentochecklistoperador2_id_operador",
                table: "equipamentochecklistoperador",
                newName: "IX_equipamentochecklistoperador_id_operador");

            migrationBuilder.RenameIndex(
                name: "IX_equipamentochecklistoperador2_id_equipamentochecklist",
                table: "equipamentochecklistoperador",
                newName: "IX_equipamentochecklistoperador_id_equipamentochecklist");

            migrationBuilder.RenameIndex(
                name: "IX_equipamentochecklistoperador2_id_equipamento",
                table: "equipamentochecklistoperador",
                newName: "IX_equipamentochecklistoperador_id_equipamento");

            migrationBuilder.AddPrimaryKey(
                name: "PK_equipamentochecklistoperador",
                table: "equipamentochecklistoperador",
                column: "id_equipamentochecklistoperador");

            migrationBuilder.AddForeignKey(
                name: "FK_equipamentochecklistoperador_equipamento_id_equipamento",
                table: "equipamentochecklistoperador",
                column: "id_equipamento",
                principalTable: "equipamento",
                principalColumn: "id_equipamento",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_equipamentochecklistoperador_equipamentochecklist_id_equipamentochecklist",
                table: "equipamentochecklistoperador",
                column: "id_equipamentochecklist",
                principalTable: "equipamentochecklist",
                principalColumn: "id_equipamentochecklist",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_equipamentochecklistoperador_operador_id_operador",
                table: "equipamentochecklistoperador",
                column: "id_operador",
                principalTable: "operador",
                principalColumn: "id_operador",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_equipamentochecklistoperador_equipamento_id_equipamento",
                table: "equipamentochecklistoperador");

            migrationBuilder.DropForeignKey(
                name: "FK_equipamentochecklistoperador_equipamentochecklist_id_equipamentochecklist",
                table: "equipamentochecklistoperador");

            migrationBuilder.DropForeignKey(
                name: "FK_equipamentochecklistoperador_operador_id_operador",
                table: "equipamentochecklistoperador");

            migrationBuilder.DropPrimaryKey(
                name: "PK_equipamentochecklistoperador",
                table: "equipamentochecklistoperador");

            migrationBuilder.RenameTable(
                name: "equipamentochecklistoperador",
                newName: "equipamentochecklistoperador2");

            migrationBuilder.RenameIndex(
                name: "IX_equipamentochecklistoperador_id_operador",
                table: "equipamentochecklistoperador2",
                newName: "IX_equipamentochecklistoperador2_id_operador");

            migrationBuilder.RenameIndex(
                name: "IX_equipamentochecklistoperador_id_equipamentochecklist",
                table: "equipamentochecklistoperador2",
                newName: "IX_equipamentochecklistoperador2_id_equipamentochecklist");

            migrationBuilder.RenameIndex(
                name: "IX_equipamentochecklistoperador_id_equipamento",
                table: "equipamentochecklistoperador2",
                newName: "IX_equipamentochecklistoperador2_id_equipamento");

            migrationBuilder.AddPrimaryKey(
                name: "PK_equipamentochecklistoperador2",
                table: "equipamentochecklistoperador2",
                column: "id_equipamentochecklistoperador");

            migrationBuilder.AddForeignKey(
                name: "FK_equipamentochecklistoperador2_equipamento_id_equipamento",
                table: "equipamentochecklistoperador2",
                column: "id_equipamento",
                principalTable: "equipamento",
                principalColumn: "id_equipamento",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_equipamentochecklistoperador2_equipamentochecklist_id_equipamentochecklist",
                table: "equipamentochecklistoperador2",
                column: "id_equipamentochecklist",
                principalTable: "equipamentochecklist",
                principalColumn: "id_equipamentochecklist",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_equipamentochecklistoperador2_operador_id_operador",
                table: "equipamentochecklistoperador2",
                column: "id_operador",
                principalTable: "operador",
                principalColumn: "id_operador",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
