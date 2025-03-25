using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIAG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EquipamentoVinculoChecklist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_equipamentochecklist_equipamentomodelo_id_equipamento",
                table: "equipamentochecklist");

            migrationBuilder.AddForeignKey(
                name: "FK_equipamentochecklist_equipamento_id_equipamento",
                table: "equipamentochecklist",
                column: "id_equipamento",
                principalTable: "equipamento",
                principalColumn: "id_equipamento",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_equipamentochecklist_equipamento_id_equipamento",
                table: "equipamentochecklist");

            migrationBuilder.AddForeignKey(
                name: "FK_equipamentochecklist_equipamentomodelo_id_equipamento",
                table: "equipamentochecklist",
                column: "id_equipamento",
                principalTable: "equipamentomodelo",
                principalColumn: "id_equipamentomodelo",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
