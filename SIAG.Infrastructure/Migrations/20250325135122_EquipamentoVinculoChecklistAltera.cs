using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIAG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EquipamentoVinculoChecklistAltera : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_equipamentochecklist_equipamento_id_equipamento",
                table: "equipamentochecklist");

            migrationBuilder.RenameColumn(
                name: "id_equipamento",
                table: "equipamentochecklist",
                newName: "id_equipamentomodelo");

            migrationBuilder.RenameIndex(
                name: "IX_equipamentochecklist_id_equipamento",
                table: "equipamentochecklist",
                newName: "IX_equipamentochecklist_id_equipamentomodelo");

            migrationBuilder.AddForeignKey(
                name: "FK_equipamentochecklist_equipamentomodelo_id_equipamentomodelo",
                table: "equipamentochecklist",
                column: "id_equipamentomodelo",
                principalTable: "equipamentomodelo",
                principalColumn: "id_equipamentomodelo",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_equipamentochecklist_equipamentomodelo_id_equipamentomodelo",
                table: "equipamentochecklist");

            migrationBuilder.RenameColumn(
                name: "id_equipamentomodelo",
                table: "equipamentochecklist",
                newName: "id_equipamento");

            migrationBuilder.RenameIndex(
                name: "IX_equipamentochecklist_id_equipamentomodelo",
                table: "equipamentochecklist",
                newName: "IX_equipamentochecklist_id_equipamento");

            migrationBuilder.AddForeignKey(
                name: "FK_equipamentochecklist_equipamento_id_equipamento",
                table: "equipamentochecklist",
                column: "id_equipamento",
                principalTable: "equipamento",
                principalColumn: "id_equipamento",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
